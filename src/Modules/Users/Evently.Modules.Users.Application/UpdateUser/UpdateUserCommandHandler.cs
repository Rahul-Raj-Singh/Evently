using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;
using MediatR;

namespace Evently.Modules.Users.Application.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) 
: IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(
        UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        user.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}