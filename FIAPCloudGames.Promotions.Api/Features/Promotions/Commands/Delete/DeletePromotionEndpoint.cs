using Carter;
using FIAPCloudGames.Promotions.Api.Commom;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Delete;

public sealed class DeletePromotionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("promotions/{id:guid}",
            async (Guid id, DeletePromotionUseCase useCase, CancellationToken cancellation) =>
            {
                await useCase.HandleAsync(id, cancellation);

                return Results.NoContent();
            })
            .WithTags(Tags.Promotions);
    }
}
