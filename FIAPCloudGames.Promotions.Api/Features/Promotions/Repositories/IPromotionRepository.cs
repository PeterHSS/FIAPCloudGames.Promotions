using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Repositories;

public interface IPromotionRepository
{
    Task<IEnumerable<Promotion>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Promotion promotion, CancellationToken cancellationToken = default);
    void Update(Promotion promotion);
    void Delete(Promotion promotion);
}
