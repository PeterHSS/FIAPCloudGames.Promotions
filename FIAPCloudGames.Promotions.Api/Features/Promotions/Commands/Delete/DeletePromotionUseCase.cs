using FIAPCloudGames.Promotions.Api.Commom.Interfaces;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Repositories;
using Serilog;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Delete;

public sealed class DeletePromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePromotionUseCase(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
    {
        _promotionRepository = promotionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Log.Information("Deleting promotion with ID {Id}", id);

        Promotion? promotion = await _promotionRepository.GetByIdAsync(id, cancellationToken);

        if (promotion is null)
        {
            Log.Warning("Promotion with ID {Id} not found", id);

            throw new KeyNotFoundException($"Promotion with ID {id} not found.");
        }

        _promotionRepository.Delete(promotion);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Promotion with ID {Id} deleted successfully", id);
    }
}
