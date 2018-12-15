namespace Desafio.Domain.Exceptions
{
    public class EmailAlreadyExistsException : DesafioException
    {
        public EmailAlreadyExistsException() : base("email-already-exists-exception", 1)
        {
        }
    }
}
