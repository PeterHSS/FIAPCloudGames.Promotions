using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Repositories;
using Serilog;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Queries.GetAll;

public sealed class GetAllPromotionsUseCase
{
    private readonly IPromotionRepository _promotionRepository;

    public GetAllPromotionsUseCase(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<IEnumerable<PromotionResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        Log.Information("Retrieving all promotions...");

        IEnumerable<Promotion> promotions = await _promotionRepository.GetAllAsync(cancellationToken);

        Log.Information("Retrieved {Count} promotions.", promotions.Count());

        return promotions.Select(PromotionResponse.Create);
    }
}
