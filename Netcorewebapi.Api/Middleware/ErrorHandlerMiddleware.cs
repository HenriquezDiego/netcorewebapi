using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Netcorewebapi.Api.Infrastructure.HttpErrors;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Netcorewebapi.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpErrorFactory _httpErrorFactory;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
       // private readonly TelemetryClient _telemetryClient;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            IHttpErrorFactory httpErrorFactory,
            ILogger<ErrorHandlerMiddleware> logger)
          //  TelemetryClient telemetryClient)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _httpErrorFactory = httpErrorFactory ?? throw new ArgumentNullException(nameof(httpErrorFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
              
                await _next(context);
            }
            catch (Exception exception)
            {
               // _telemetryClient.TrackException(exception);
                _logger.LogError(exception.HResult, exception, exception.Message);
                await CreateHttpError(context, exception);
            }
        }

        private async Task CreateHttpError(HttpContext context, Exception exception)
        {
            var error = _httpErrorFactory.CreateFrom(exception);

            await WriteResponseAsync(
                context,
                JsonConvert.SerializeObject(error),
                "application/json",
                error.Status);
        }

        private static Task WriteResponseAsync(
           HttpContext context,
           string content,
           string contentType,
           int statusCode)
        {
            context.Response.Headers["Content-Type"] = new[] { contentType };
            context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store, must-revalidate" };
            context.Response.Headers["Pragma"] = new[] { "no-cache" };
            context.Response.Headers["Expires"] = new[] { "0" };
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(content);
        }
    }
}
