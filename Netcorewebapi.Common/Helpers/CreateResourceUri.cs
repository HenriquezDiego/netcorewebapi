using Microsoft.AspNetCore.Mvc;
using Netcorewebapi.Common.Helpers;

namespace Netcorewebapi.Common
{
    public class CreateResourceUri
    {
        private readonly IUrlHelper _urlHelper;
        public CreateResourceUri(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public string CreateProductResourceUri(
            ProductParameters resourceParameters,
            ResourceUriType type,
            string routeName)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link(routeName,
                      new
                      {
                          page = resourceParameters.Page - 1,
                          perpage = resourceParameters.PerPage
                      });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link(routeName,
                      new
                      {
                          page = resourceParameters.Page + 1,
                          perpage = resourceParameters.PerPage
                      });

                default:
                    return _urlHelper.Link(routeName,
                    new
                    {
                        page = resourceParameters.Page,
                        perpage = resourceParameters.PerPage
                    });
            }
        }
    }
}
