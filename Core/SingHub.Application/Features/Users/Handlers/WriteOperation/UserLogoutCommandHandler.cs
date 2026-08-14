using MediatR;
using Microsoft.AspNetCore.Identity;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.Users.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.Users.Handlers.WriteOperation;

public class UserLogoutCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
    : IRequestHandler<UserLogoutCommand, string>
{
    public async Task<string> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
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
