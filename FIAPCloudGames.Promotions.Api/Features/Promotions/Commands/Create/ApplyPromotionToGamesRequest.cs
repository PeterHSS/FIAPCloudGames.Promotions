namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Create;

public record ApplyPromotionToGamesRequest(Guid PromotionId, IEnumerable<Guid> GamesId);
