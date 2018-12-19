using System;

namespace Netcorewebapi.Api.Infrastructure.HttpErrors
{
    public interface IHttpErrorFactory
    {
        HttpError CreateFrom(Exception exception);
    }
}
