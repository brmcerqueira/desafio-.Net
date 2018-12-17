namespace Desafio.Domain.Exceptions
{
    public class AuthenticationException : DesafioException
    {
        public AuthenticationException() : base("AuthenticationException", 2)
        {
        }
    }
}
