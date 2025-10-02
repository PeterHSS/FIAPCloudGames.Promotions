using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Queries;

public sealed record PromotionResponse(Guid Id, string Name, DateTime StartDate, DateTime EndDate, decimal DiscountPercentage, string Description, IEnumerable<GameResponse> Games)
{
    public static PromotionResponse Create(Promotion promotion, IEnumerable<GameResponse> game)
    {
        return new(promotion.Id, promotion.Name, promotion.StartDate, promotion.EndDate, promotion.DiscountPercentage, promotion.Description, game);
    }

    public static PromotionResponse Create(Promotion promotion)
    {
        return new(promotion.Id, promotion.Name, promotion.StartDate, promotion.EndDate, promotion.DiscountPercentage, promotion.Description, []);
    }
}
