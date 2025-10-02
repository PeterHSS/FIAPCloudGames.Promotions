namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Create;

public record CreatePromotionRequest(string Name, DateTime StartDate, DateTime EndDate, decimal DiscountPercentage, string Description, IEnumerable<Guid>? GamesId);