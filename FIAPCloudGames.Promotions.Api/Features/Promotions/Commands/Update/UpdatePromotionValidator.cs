using FIAPCloudGames.Promotions.Api.Features.Promotions;

namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Update;

public sealed class UpdatePromotionValidator : AbstractPromotionValidator<UpdatePromotionRequest>
{
    public UpdatePromotionValidator()
    {
        AddNameRule(request => request.Name);

        AddStartDateRule(request => request.StartDate);

        AddEndDateRule(request => request.EndDate, request => request.StartDate);

        AddDiscountPercentageRule(request => request.DiscountPercentage);

        AddDescriptionRule(request => request.Description);
    }
}
