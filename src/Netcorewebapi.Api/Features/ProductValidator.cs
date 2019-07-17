
using FluentValidation;
using Netcorewebapi.DataAccess.Entities;

namespace Netcorewebapi.Api.Features
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(40);
        }
    }
}
