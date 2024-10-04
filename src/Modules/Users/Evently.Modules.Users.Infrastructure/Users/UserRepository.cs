using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Users.Infrastructure.Users;

internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        foreach (var role in user.Roles)
        {
            // role is already seeded in db. Ask EF to not insert new row
            context.Attach(role);
        }

        context.Users.Add(user);
    }
}
