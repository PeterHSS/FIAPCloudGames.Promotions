
using FIAPCloudGames.Promotions.Api.Features.Promotions.Queries;

namespace FIAPCloudGames.Promotions.Api.Commom.Interfaces;

public interface IGameServiceClient
{
    Task<bool> ApplyPromotion(Guid promotionId, IEnumerable<Guid> gamesId);
    Task<IEnumerable<GameResponse>> GetGamesByPromotionIdAsync(Guid id);
}
