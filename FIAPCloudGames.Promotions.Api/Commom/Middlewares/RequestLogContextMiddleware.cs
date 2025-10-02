using Serilog.Context;

namespace FIAPCloudGames.Promotions.Api.Commom.Middlewares;

public class RequestLogContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            return _next(context);
        }
    }
}
