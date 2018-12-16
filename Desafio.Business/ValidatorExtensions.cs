using FluentValidation;
using LightInject;
using System.Linq;

namespace Desafio.Business
{
    public static class ValidatorExtensions
    {       
        public static void AdjustValidationLanguageManager(this IServiceFactory serviceFactory)
        {
            ValidatorOptions.LanguageManager = serviceFactory.GetInstance<ValidationLanguageManager>();
        }

        internal static void Check<T>(this IValidator<T> validator, T instance)
        {
            var validationResult = validator.Validate(instance);

            if (!validationResult.IsValid)
            {
                throw new Domain.Exceptions.ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }
        }
    }
}
