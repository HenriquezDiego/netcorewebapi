using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.Common;
using Netcorewebapi.Common.Helpers;
using System;

namespace Netcorewebapi.Api.Helpers
{
    public class CreateResourceUri
    {
        private readonly IUrlHelper _urlHelper;
        public CreateResourceUri(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public Uri CreateProductResourceUri(
            ProductParameters resourceParameters,
            ResourceUriType type,
            string routeName)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return new Uri(_urlHelper.Link(routeName,
                      new
                      {
                          page = resourceParameters?.Page - 1,
                          perpage = resourceParameters.PerPage
                      }));
                case ResourceUriType.NextPage:
                    return new Uri(_urlHelper.Link(routeName,
                      new
                      {
                          page = resourceParameters?.Page + 1,
                          perpage = resourceParameters.PerPage
                      }));

                default:
                    return new Uri(_urlHelper.Link(routeName,
                    new
                    {
                        page = resourceParameters?.Page,
                        perpage = resourceParameters.PerPage
                    }));
            }
        }
    }
}
