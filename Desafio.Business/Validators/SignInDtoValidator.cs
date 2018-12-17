using Desafio.Business.Dtos;
using FluentValidation;

namespace Desafio.Business
{
    internal class SignInDtoValidator : AbstractValidator<ISignInDto>
    {
        public SignInDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Password).NotNull().MinimumLength(7).MaximumLength(30);
        }
    }
}