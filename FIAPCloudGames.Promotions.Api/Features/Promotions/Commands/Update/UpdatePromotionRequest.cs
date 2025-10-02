namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Update;

public record UpdatePromotionRequest(string Name, DateTime StartDate, DateTime EndDate, decimal DiscountPercentage, string Description);
