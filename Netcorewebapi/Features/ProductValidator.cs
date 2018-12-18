
using FluentValidation;
using Netcorewebapi.DataAccess.Data.Entities;

namespace Netcorewebapi.Features
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
