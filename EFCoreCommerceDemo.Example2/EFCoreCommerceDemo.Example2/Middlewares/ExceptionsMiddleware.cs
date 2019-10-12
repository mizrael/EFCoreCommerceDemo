using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EFCoreCommerceDemo.Example2.Middlewares
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(RequestDelegate next,  ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }      

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ExtractHttpStatus(ex);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(ex.Message);
            
            _logger.LogError(ex, ex.Message);
        }

        private static int ExtractHttpStatus<TEx>(TEx ex) where TEx : Exception
        {
            var status = HttpStatusCode.InternalServerError;

            if (ex is ArgumentNullException ||
                ex is ArgumentOutOfRangeException ||
                ex is ArgumentException ||
                ex is HttpRequestException)
                status = HttpStatusCode.BadRequest;
            else if (ex is AuthenticationException)
                status = HttpStatusCode.Unauthorized;
            else if(ex is UnauthorizedAccessException)
                status = HttpStatusCode.Forbidden;

            return (int)status;
        }
    }
}
