using MediatR;
using Microsoft.AspNetCore.Identity;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.Users.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.Users.Handlers.WriteOperation;

public class ForgotPasswordCommandHandle(UserManager<AppUser> userManager, IMailService mailService)
    : IRequestHandler<ForgotPasswordCommand, BaseResult<string>>
{
    public async Task<BaseResult<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return BaseResult<string>.Failure("Email address cound not found in system");

        string resetCode = new Random().Next(100000, 999999).ToString();

        user.PasswordResetCode = resetCode;
        user.PasswordResetCodeExpiresAt = DateTime.UtcNow.AddMinutes(5);

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return BaseResult<string>.Failure("Kod üretilirken bir hata oluştu.");
        }

        _ = Task.Run(async () =>
        {
            await mailService.SendForgotPasswordCodeAsync(user.Email!, resetCode);
        });

        return BaseResult<string>.Success("Doğrulama kodu e-posta adresinize gönderildi.");
    }
}
