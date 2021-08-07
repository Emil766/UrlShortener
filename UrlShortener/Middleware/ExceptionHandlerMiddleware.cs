using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ILive.ParkCloud.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(
            ILogger logger,
            RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                if (Guid.TryParse(context.Request.Headers["RequestId"], out var requestId))
                {
                    _logger.Error(ex, $"Internal Error. RequestId : {requestId}");
                }
                else
                {
                    _logger.Error(ex, "Internal Error");
                }
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
