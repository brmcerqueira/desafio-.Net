using System;

namespace Desafio.Domain.Exceptions
{
    public abstract class DesafioException : Exception
    {
        public int Code { get; private set; }

        public DesafioException(string message, int code) : base(message)
        {
            Code = code;
        }
    }
}
