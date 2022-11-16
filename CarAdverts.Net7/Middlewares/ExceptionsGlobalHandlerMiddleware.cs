using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Domain.Core.Validation;
using System.Net;
using System.Text.Json;

namespace CarAdverts.Net7.Middlewares;

public class ExceptionsGlobalHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionsGlobalHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = "Unexpected error";

        if (exception is ValidationException validationException)
        {
            message = $"Validation error for {validationException.ParamName}: {validationException.Message}";
            statusCode = (int)HttpStatusCode.BadRequest;
        }

        if (exception is NoEntryException<Guid> noEntryException)
        {
            message = $"Bad request: {noEntryException.Message}. Key: {noEntryException.Id}";
            statusCode = (int)HttpStatusCode.BadRequest;
        }

        response.ContentType = "application/json";
        response.StatusCode = statusCode;
        await response.WriteAsync(JsonSerializer.Serialize(message));
    }
}
