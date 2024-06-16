using Evently.Common.Domain;
using MediatR;

namespace Evently.Modules.Users.Application.GetUser;

public class GetUserQuery : IRequest<Result<UserResponse>>
{
    public Guid UserId { get; set;}
}
