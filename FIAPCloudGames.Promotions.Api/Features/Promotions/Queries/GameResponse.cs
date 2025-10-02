namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Queries;

public record GameResponse(Guid Id, string Name, string Description, DateTime ReleasedAt, decimal Price, string Genre);
