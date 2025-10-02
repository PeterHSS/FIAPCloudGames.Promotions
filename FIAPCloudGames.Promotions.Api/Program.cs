using Carter;
using FIAPCloudGames.Promotions.Api;
using FIAPCloudGames.Promotions.Api.Commom.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapCarter();

app.MapControllers();

app.Run();