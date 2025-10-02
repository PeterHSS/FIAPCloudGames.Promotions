using FIAPCloudGames.Promotions.Api.Commom.Interfaces;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Repositories;
using Serilog;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Queries.GetById;

public sealed class GetPromotionByIdUseCase(IPromotionRepository promotionRepository, IGameServiceClient gameServiceClient)
{
    public async Task<PromotionResponse> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Log.Information("Retrieving promotion with ID {Id}.", id);

        Promotion? promotion = await promotionRepository.GetByIdAsync(id, cancellationToken);

        if (promotion is null)
        {
            Log.Warning("Promotion with ID {Id} not found.", id);

            throw new KeyNotFoundException($"Promotion with ID {id} not found.");
        }

        IEnumerable<GameResponse> games = await gameServiceClient.GetGamesByPromotionIdAsync(promotion.Id);

        PromotionResponse promotionResponse = PromotionResponse.Create(promotion, games);

        Log.Information("Promotion with ID {PromotionId} retrieved successfully.", id);

        return promotionResponse;
    }
}
