using Desafio.Business.Dtos;
using FluentValidation;

namespace Desafio.Business.Validators
{
    class PhoneDtoValidator : AbstractValidator<IPhoneDto>
    {
        public PhoneDtoValidator()
        {
            RuleFor(x => x.AreaCode).InclusiveBetween(1, 99);
            RuleFor(x => x.CountryCode).NotNull().MinimumLength(3).MaximumLength(3);
            RuleFor(x => x.Number).InclusiveBetween(10000000, 999999999);
        }
    }
}
