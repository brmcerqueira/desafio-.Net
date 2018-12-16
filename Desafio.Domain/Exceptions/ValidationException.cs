using System.Collections.Generic;

namespace Desafio.Domain.Exceptions
{
    public class ValidationException : DesafioException
    {
        public ValidationException(IEnumerable<string> errors) : base("ValidationException", 2)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
