using System.Data;
using FIAPCloudGames.Promotions.Api.Commom.Interfaces;
using FIAPCloudGames.Promotions.Api.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace FIAPCloudGames.Promotions.Api.Infrastructure.Persistence.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly PromotionsDbContext _context;

    public UnitOfWork(PromotionsDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IDbTransaction BeginTransaction(CancellationToken cancellationToken = default)
    {
        IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction();

        return dbContextTransaction.GetDbTransaction();
    }
}
