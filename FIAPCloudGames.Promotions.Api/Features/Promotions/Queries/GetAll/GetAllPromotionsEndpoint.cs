using Carter;
using FIAPCloudGames.Promotions.Api.Commom;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Queries.GetAll;

public sealed class GetAllPromotionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("promotions",
            async (GetAllPromotionsUseCase useCase, CancellationToken cancellationToken) =>
            {
                IEnumerable<PromotionResponse> response = await useCase.HandleAsync(cancellationToken);

                return Results.Ok(response);
            })
            .WithTags(Tags.Promotions);
    }
}
