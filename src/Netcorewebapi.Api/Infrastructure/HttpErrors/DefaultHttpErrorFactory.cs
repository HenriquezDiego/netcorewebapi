using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net;

namespace Netcorewebapi.Api.Infrastructure.HttpErrors
{
    public class DefaultHttpErrorFactory : IHttpErrorFactory
    {
        private readonly IHostEnvironment _env;
        private readonly IDictionary<Type, Func<Exception, HttpError>> _factory;

        public DefaultHttpErrorFactory(IHostEnvironment env)
        {
            _env = env;

            _factory = new Dictionary<Type, Func<Exception, HttpError>>
            {
                { typeof(Exception), InternalServerError }
            };
        }

        public HttpError CreateFrom(Exception exception)
        {
            if (_factory.TryGetValue(exception.GetType(), out Func<Exception, HttpError> func))
            {
                return func(exception);
            }

            return _factory[typeof(Exception)](exception);
        }

        private HttpError InternalServerError(Exception exception)
        {
            return HttpError.Create(
                _env,
                status: HttpStatusCode.InternalServerError,
                code: string.Empty,
                userMessage: new[] { "Internal Server Error" },
                developerMessage: $"{exception.Message}\r\n{exception.StackTrace}");
        }
    }
}
