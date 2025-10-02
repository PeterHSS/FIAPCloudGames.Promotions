using FIAPCloudGames.Promotions.Api.Commom.Interfaces;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Create;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Queries;
using Serilog;

namespace FIAPCloudGames.Promotions.Api.Infrastructure.Services;

public sealed class GameServiceClient(HttpClient httpClient) : IGameServiceClient
{
    public async Task<bool> ApplyPromotion(Guid promotionId, IEnumerable<Guid> gamesId)
    {
        try
        {
            var payload = new ApplyPromotionToGamesRequest(promotionId, gamesId);

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("games/apply-promotion", payload);

            response.EnsureSuccessStatusCode();

            return true;
        }
        catch (Exception ex)
        {
            Log.Warning("Failed to apply promotion {PromotionId} to games. Error: {ErrorMessage}", promotionId, ex.Message);

            return false;
        }
    }

    public async Task<IEnumerable<GameResponse>> GetGamesByPromotionIdAsync(Guid id)
    {
        try
        {
            IEnumerable<GameResponse>? response = await httpClient.GetFromJsonAsync<IEnumerable<GameResponse>>($"/api/v1/games/promotion/{id}");

            return response ?? [];
        }
        catch (Exception)
        {
            Log.Warning("Failed to retrieve games for promotion {PromotionId}.", id);

            return [];
        }
    }
}
