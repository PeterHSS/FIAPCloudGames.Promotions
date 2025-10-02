using System.Reflection;
using FIAPCloudGames.Promotions.Api.Commom.Interfaces;
using FIAPCloudGames.Promotions.Api.Commom.Middlewares;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Create;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Delete;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Commands.Update;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Models;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Queries.GetAll;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Queries.GetById;
using FIAPCloudGames.Promotions.Api.Features.Promotions.Repositories;
using FIAPCloudGames.Promotions.Api.Infrastructure.Persistence.Context;
using FIAPCloudGames.Promotions.Api.Infrastructure.Persistence.Repositories;
using FIAPCloudGames.Promotions.Api.Infrastructure.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FIAPCloudGames.Promotions.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddUseCases()
            .AddValidators()
            .AddHttpClients(configuration);

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpClient<IGameServiceClient, GameServiceClient>(httpClient =>
            {
                var baseUrl = configuration.GetValue<string>("GamesApi:BaseUrl")!;

                httpClient.BaseAddress = new Uri(baseUrl);

            });

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddProblemDetails(configure =>
        {
            configure.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
            };
        });

        services.AddExceptionHandler<ValidationExceptionHandlerMiddleware>();

        services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRepositories(configuration);

        return services;
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<PromotionsDbContext>();

        dbContext.Database.Migrate();
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreatePromotionUseCase>();

        services.AddScoped<UpdatePromotionUseCase>();
        
        services.AddScoped<GetPromotionByIdUseCase>();

        services.AddScoped<DeletePromotionUseCase>();

        services.AddScoped<GetAllPromotionsUseCase>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped, includeInternalTypes: true);

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PromotionsDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(nameof(Promotion))));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPromotionRepository, PromotionRepository>();

        return services;
    }
}
