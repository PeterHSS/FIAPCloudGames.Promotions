using FIAPCloudGames.Promotions.Api.Features.Promotions;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Create;

public sealed class CreatePromotionValidator : AbstractPromotionValidator<CreatePromotionRequest>
{
    public CreatePromotionValidator()
    {
        AddNameRule(request => request.Name);

        AddStartDateRule(request => request.StartDate);

        AddEndDateRule(request => request.EndDate, request => request.StartDate);

        AddDiscountPercentageRule(request => request.DiscountPercentage);

        AddDescriptionRule(request => request.Description);
    }
}
