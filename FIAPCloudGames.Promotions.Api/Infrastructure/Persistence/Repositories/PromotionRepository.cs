using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Repositories;
using FIAPCloudGames.Promotions.Api.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Promotions.Api.Infrastructure.Persistence.Repositories;

internal sealed class PromotionRepository : IPromotionRepository
{
    private readonly PromotionsDbContext _context;

    public PromotionRepository(PromotionsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Promotion promotion, CancellationToken cancellationToken = default) 
        => await _context.Promotions.AddAsync(promotion, cancellationToken);

    public void Delete(Promotion promotion) 
        => _context.Promotions.Remove(promotion);

    public async Task<IEnumerable<Promotion>> GetAllAsync(CancellationToken cancellationToken = default) 
        => await _context.Promotions.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) 
        => await _context.Promotions.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public void Update(Promotion promotion) 
        => _context.Promotions.Update(promotion);
}
