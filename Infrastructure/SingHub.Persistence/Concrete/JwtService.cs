using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.Users.Result;
using SingHub.Application.Options;
using SingHub.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SingHub.Persistence.Concrete;

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
            new(JwtRegisteredClaimNames.UniqueName,user.UserName!),
            new(JwtRegisteredClaimNames.Sub,user.Id.ToString()!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new("FullName",string.Join(" ",user.Name,user.Surname)),
        };

        foreach (var role in roles)
        {
            claims.Add(new("role", role));
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
            ExpirationTime = expiration
        };

        return reponse;
    }
}
