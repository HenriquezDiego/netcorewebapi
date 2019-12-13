
using FluentValidation;
using Netcorewebapi.DataAccess.Entities;

namespace Netcorewebapi.Api.Features
{
    public class ProductValidatorCollection : AbstractValidator<Product>
    {
        public ProductValidatorCollection()
        {
            RuleFor(product => product.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(40);
        }
    }
}
