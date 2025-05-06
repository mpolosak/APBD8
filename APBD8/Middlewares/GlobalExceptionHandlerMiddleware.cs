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
            await HandleExceptionAsync(context, ex);
        }
    }

    static async Task HandleExceptionAsync(HttpContext context,  Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/text";
        await context.Response.WriteAsync(ex.Message);
    }
}