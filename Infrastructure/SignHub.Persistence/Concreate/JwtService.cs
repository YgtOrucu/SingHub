using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SignHub.Application.Contract.Persistence;
using SignHub.Application.Exceptions;
using SignHub.Application.Features.Users.Result;
using SignHub.Application.Options;
using SignHub.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignHub.Persistence.Concreate;

public class JwtService(UserManager<AppUser> userManager, IOptions<JwtTokenOptions> options) : IJwtService
{
    private readonly JwtTokenOptions _jwtToken = options.Value;
    public async Task<GetLoginQueryResult> GenerateTokenAsync(string UserName)
    {
        var user = await userManager.FindByNameAsync(UserName);
        if (user == null)
            throw new NotFoundException("User not Found");

        var roles = await userManager.GetRolesAsync(user);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtToken.Key));
        var now = DateTime.UtcNow;
        var expiration = now.AddMinutes(_jwtToken.ExpireInMinutes);

        List<Claim> claims = new()
        {
            new("UserName",user.UserName!),
            new("UserId",user.Id.ToString()!),
            new("FullName",string.Join(" ",user.Name,user.Surname)),
            new("AvatarUrl",user.AvatarUrl!),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }


        JwtSecurityToken jwtSecurityToken = new
        (
            issuer: _jwtToken.Issuer,
            audience: _jwtToken.Audience,
            claims: claims,
            notBefore: now,
            expires: expiration,
            signingCredentials: new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        GetLoginQueryResult reponse = new GetLoginQueryResult()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            ExprationTime = expiration
        };

        return reponse;
    }
}
