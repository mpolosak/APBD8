using APBD8.Exceptions;

namespace APBD8.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next,  ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            logger.LogError(ex, "Not found exception");
            await HandleNotFoundExceptionAsync(context, ex);
        }
        catch (ConflictException ex)
        {
            logger.LogError(ex, "Conflict exception");
            await HandleConflictExceptionAsync(context, ex);
        }
    }

    private static async Task HandleNotFoundExceptionAsync(HttpContext context,  Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/text";
        await context.Response.WriteAsync(ex.Message);
    }
    private static async Task HandleConflictExceptionAsync(HttpContext context,  Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status409Conflict;
        context.Response.ContentType = "application/text";
        await context.Response.WriteAsync(ex.Message);
    }
}