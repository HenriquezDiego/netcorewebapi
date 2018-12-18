using System;

namespace Netcorewebapi.Infrastructure.HttpErrors
{
    public interface IHttpErrorFactory
    {
        HttpError CreateFrom(Exception exception);
    }
}
