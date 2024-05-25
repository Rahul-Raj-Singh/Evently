using System.Data.Common;

namespace Evently.Modules.Events.Application.Abstractions.Data;

public interface IDbConnectionFactory
{
    Task<DbConnection> OpenConnectionAsync();
}
