using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SignHub.Application.Bases;
using SignHub.Application.Contract.Persistence;
using SignHub.Application.Exceptions;
using SignHub.Application.Features.Users.Commands;
using SignHub.Domain.Entities;

namespace SignHub.Application.Features.Users.Handlers.WriteOperation;

public class CreateUserCommandHandle(UserManager<AppUser> userManager, IMapper mapper, IMailService mailService)
    : IRequestHandler<CreateUserCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<AppUser>(request);
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BaseResult<object>.Failure(result.Errors);

        _ = Task.Run(async () =>
        {
            try
            {
                await mailService.SendMail(user.Name, user.Surname, user.Email!);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

        });

        return BaseResult<object>.Success(result);
    }
}
