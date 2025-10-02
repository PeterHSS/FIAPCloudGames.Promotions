using System.Data;

namespace FIAPCloudGames.Promotions.Api.Commom.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BeginTransaction(CancellationToken cancellationToken = default);
}
