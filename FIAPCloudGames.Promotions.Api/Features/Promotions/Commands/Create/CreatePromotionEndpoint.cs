using Carter;
using FIAPCloudGames.Promotions.Api.Commom;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Create;

public sealed class CreatePromotionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("promotions",
            async ([FromBody] CreatePromotionRequest request,
                   [FromServices] CreatePromotionUseCase useCase,
                   [FromServices] IValidator<CreatePromotionRequest> validator,
                   CancellationToken cancellationToken) =>
            {
                validator.ValidateAndThrow(request);

                await useCase.HandleAsync(request, cancellationToken);

                return Results.Created();
            })
            .WithTags(Tags.Promotions);
    }
}
