using Desafio.Business.Dtos;
using FluentValidation;

namespace Desafio.Business.Validators
{
    internal class SignUpDtoValidator : AbstractValidator<ISignUpDto>
    {
        public SignUpDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.FirstName).NotNull().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.LastName).NotNull().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Password).NotNull().MinimumLength(7).MaximumLength(30);
            RuleForEach(x => x.Phones).NotNull().NotEmpty().SetValidator(new PhoneDtoValidator());
        }
    }
}
