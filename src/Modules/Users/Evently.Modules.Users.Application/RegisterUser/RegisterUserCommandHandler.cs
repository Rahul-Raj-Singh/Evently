using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Evently.Modules.Users.Domain.Users;
using MediatR;

namespace Evently.Modules.Users.Application.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository, 
    IIdentityProviderService idpService, 
    IUnitOfWork unitOfWork) 
: IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await idpService.RegisterUserAsync(
            request.Email, request.Password, request.FirstName, request.LastName);

        if (!result.IsSuccess)
        {
            return Result.Failure<Guid>(result.Error);
        }

        var user = User.Create(request.FirstName, request.LastName, request.Email, result.Value);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}