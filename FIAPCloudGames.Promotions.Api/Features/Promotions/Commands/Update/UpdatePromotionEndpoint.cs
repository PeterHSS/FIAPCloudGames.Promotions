using Carter;
using FIAPCloudGames.Promotions.Api.Commom;
using FluentValidation;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Update;

public sealed class UpdatePromotionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("promotions/{id:guid}",
            async (Guid id, UpdatePromotionRequest request, UpdatePromotionUseCase useCase, IValidator<UpdatePromotionRequest> validator, CancellationToken cancellationToken) =>
            {
                validator.ValidateAndThrow(request);

                await useCase.HandleAsync(id, request, cancellationToken);

                return Results.NoContent();
            })
            .WithTags(Tags.Promotions);
    }
}