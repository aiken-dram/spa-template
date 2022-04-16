using System.Net;
using Newtonsoft.Json;
using Shared.Application.Exceptions;

namespace WebApi.Common;

// Source: Clean Architecture example (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.Failures);
                break;
            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { Error = badRequestException.Message });
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            case AccessDeniedException accessDeniedException:
                code = HttpStatusCode.Forbidden;
                result = JsonConvert.SerializeObject(new { Error = accessDeniedException.Message });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonConvert.SerializeObject(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}
