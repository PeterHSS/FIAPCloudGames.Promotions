using Carter;
using FIAPCloudGames.Promotions.Api.Commom;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Queries.GetById;

public sealed class GetPromotionByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("promotions/{id:guid}",
            async (Guid id, GetPromotionByIdUseCase useCase, CancellationToken cancellationToken) =>
            {
                PromotionResponse response = await useCase.HandleAsync(id, cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Promotions)
            .RequireAuthorization();
    }
}
