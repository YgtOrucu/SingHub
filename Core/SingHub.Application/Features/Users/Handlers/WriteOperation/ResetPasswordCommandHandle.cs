using MediatR;
using Microsoft.AspNetCore.Identity;
using SingHub.Application.Bases;
using SingHub.Application.Features.Users.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.Users.Handlers.WriteOperation;

public class ResetPasswordCommandHandle(UserManager<AppUser> userManager)
: IRequestHandler<ResetPasswordCommand, BaseResult<string>>
{
    public async Task<BaseResult<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.NewPassword != request.ConfirmPassword)
        {
            return BaseResult<string>.Failure("Şifreler birbiriyle uyuşmuyor.");
        }

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return BaseResult<string>.Failure("Geçersiz e-posta veya kod.");
        }

        if (string.IsNullOrEmpty(user.PasswordResetCode) ||
            user.PasswordResetCode != request.Code ||
            user.PasswordResetCodeExpiresAt == null ||
            user.PasswordResetCodeExpiresAt < DateTime.UtcNow)
        {
            return BaseResult<string>.Failure("Kod geçersiz veya süresi dolmuş. Lütfen tekrar kod isteyin.");
        }
  
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (!resetResult.Succeeded)
        { 
            return BaseResult<string>.Failure(resetResult.Errors);
        }

        user.PasswordResetCode = null;
        user.PasswordResetCodeExpiresAt = null;

        await userManager.UpdateSecurityStampAsync(user);
        await userManager.UpdateAsync(user);

        return BaseResult<string>.Success("Şifreniz başarıyla güncellendi. Yeni şifrenizle giriş yapabilirsiniz.");
    }
}

