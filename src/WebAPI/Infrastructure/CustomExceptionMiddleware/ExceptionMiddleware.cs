using Application.GlobalErrorHandlong;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(NotYourObject nyo)
            {
                _logger.LogError($"A new NotYourObject exception has been thrown: {nyo}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await HandleExceptionAsync(httpContext, nyo);
            }
            catch (AccessViolationException avEx)
            {
                _logger.LogError($"A new violation exception has been thrown: {avEx}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(httpContext, avEx);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception, string details = "")
        {
            context.Response.ContentType = "application/json";
            
            var message = exception switch
            {
                AccessViolationException => "Access violation error from the custom middleware",
                NotYourObject => "Not Your Object",
                _ => "Internal Server Error from the custom middleware.",
            };
            await context.Response.WriteAsync(new ApiErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Details = exception.Message
            }.ToString());
        }
    }
}
