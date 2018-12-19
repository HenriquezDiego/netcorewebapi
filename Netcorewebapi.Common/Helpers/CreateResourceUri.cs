using Microsoft.AspNetCore.Mvc;

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
            ResourceParameters resourceParameters,
            ResourceUriType type,
            string routeName)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link(routeName,
                      new
                      {
                         
                          pageNumber = resourceParameters.PageNumber - 1,
                          pageSize = resourceParameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link(routeName,
                      new
                      {
                        
                          pageNumber = resourceParameters.PageNumber + 1,
                          pageSize = resourceParameters.PageSize
                      });

                default:
                    return _urlHelper.Link(routeName,
                    new
                    {
                        
                        pageNumber = resourceParameters.PageNumber,
                        pageSize = resourceParameters.PageSize
                    });
            }
        }
    }
}
