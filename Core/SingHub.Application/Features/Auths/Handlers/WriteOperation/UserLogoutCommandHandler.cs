using MediatR;
using Microsoft.AspNetCore.Identity;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.Auths.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.Auths.Handlers.WriteOperation;

public class UserLogoutCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
    : IRequestHandler<LogoutCommand, string>
{
    public async Task<string> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user != null)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();

            await userManager.UpdateAsync(user);
        }

        return "Çıkış başarılı, token geçersiz kılındı.";
    }
}
