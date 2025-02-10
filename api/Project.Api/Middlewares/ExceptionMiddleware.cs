using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Project.Core.Exceptions;

namespace api;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Validation error: {Message}", ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new ApiExceptionResponse(context.Response.StatusCode, "Wprowadzone dane są nieporpawne", ex.ValidationResult?.ErrorMessage);

            await GenerateResponse(context, response);
        }
        catch (ApiControlledException ex)
        {
            _logger.LogError(ex, ex.Message);
            context = SetResponseStatus(context, ex);

            var response = new ApiExceptionResponse(context.Response.StatusCode, ex.Message, ex.Details);

            await GenerateResponse(context, response);
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var response = new ApiExceptionResponse(context.Response.StatusCode, ex.Message, "Nie znaleziono pliku!");

            await GenerateResponse(context, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
            ? new ApiExceptionResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
            : new ApiExceptionResponse(context.Response.StatusCode, ex.Message, "Błąd serwera");

            await GenerateResponse(context, response);
        }
    }

    private async Task GenerateResponse(HttpContext context, ApiExceptionResponse ex)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var json = JsonSerializer.Serialize(ex, options);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(json);
    }

    private HttpContext SetResponseStatus(HttpContext context, ApiControlledException ex)
    {
        switch(ex.StatusCode)
        {
            case 401:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case 404: 
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case 409:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
        }
         return context;
    }
}