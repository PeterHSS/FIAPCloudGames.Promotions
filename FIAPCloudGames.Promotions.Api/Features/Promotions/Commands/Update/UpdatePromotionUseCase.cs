using FIAPCloudGames.Promotions.Api.Commom.Interfaces;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Repositories;
using Serilog;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Update;

public sealed class UpdatePromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePromotionUseCase(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
    {
        _promotionRepository = promotionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid id, UpdatePromotionRequest request, CancellationToken cancellationToken = default)
    {
        Log.Information("Start updating promotion. {@PromotionId} {@Request}", id, request);

        Promotion? promotion = await _promotionRepository.GetByIdAsync(id, cancellationToken);

        if (promotion is null)
        {
            Log.Warning("Promotion not found. {@PromotionId}", id);

            throw new KeyNotFoundException($"Promotion with ID {id} not found.");
        }

        promotion.Update(request.Name, request.StartDate, request.EndDate, request.DiscountPercentage, request.Description);

        _promotionRepository.Update(promotion);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Promotion updated successfully. {@PromotionId}", id);
    }
}
