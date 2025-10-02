using System.Data;
using FIAPCloudGames.Promotions.Api.Commom.Interfaces;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Repositories;
using Serilog;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Create;

public sealed class CreatePromotionUseCase(
    IPromotionRepository promotionRepository,
    IGameServiceClient gameService,
    IUnitOfWork unitOfWork)
{
    public async Task HandleAsync(CreatePromotionRequest request, CancellationToken cancellationToken = default)
    {
        using IDbTransaction transaction = unitOfWork.BeginTransaction(cancellationToken);

        Log.Information("Starting transaction for creating promotion with name: {PromotionName}", request.Name);

        try
        {
            Promotion promotion = Promotion.Create(request.Name, request.StartDate, request.EndDate, request.DiscountPercentage, request.Description);

            await promotionRepository.AddAsync(promotion, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            Log.Information("Promotion created successfully with ID: {PromotionId}", promotion.Id);

            if (request.GamesId?.Any() ?? false)
            {
                bool promotionApplied = await gameService.ApplyPromotion(promotion.Id, request.GamesId);

                if (!promotionApplied)
                {
                    Log.Error("Failed to apply promotion with ID: {PromotionId} to games", promotion.Id);
                 
                    throw new Exception("Failed to apply promotion to games");
                }

                Log.Information("Games updated with promotion successfully");
            }

            transaction.Commit();

            Log.Information("Transaction committed successfully for creating promotion with name: {PromotionName}", request.Name);
        }
        catch (Exception)
        {
            transaction.Rollback();

            Log.Warning("Transaction rolled back due to an error while creating promotion with name: {PromotionName}", request.Name);

            throw;
        }
    }
}
