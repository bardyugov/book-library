namespace BookLibrary.Core.Middlewares;

public static class ExceptionsFilterHandlerExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionsFilterHandler>();
    }
}