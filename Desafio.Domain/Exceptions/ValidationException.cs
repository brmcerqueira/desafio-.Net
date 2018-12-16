using System.Collections.Generic;

namespace Desafio.Domain.Exceptions
{
    public class ValidationException : DesafioException
    {
        public ValidationException(IEnumerable<string> errors) : base("validation_exception", 2)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
