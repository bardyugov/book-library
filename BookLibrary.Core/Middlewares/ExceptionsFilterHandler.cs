using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace BookLibrary.Core.Middlewares;

public class ExceptionsFilterHandler
{
    private readonly RequestDelegate _next;

    public ExceptionsFilterHandler(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleException(exception, context);
        }
    }


    private Task HandleException(Exception exception, HttpContext context)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
            
        if (exception is ValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            Console.WriteLine(JsonSerializer.Serialize(validationException.Errors));
            result = JsonSerializer.Serialize(validationException.Errors);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
            
        if (result.IsNullOrEmpty())
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}