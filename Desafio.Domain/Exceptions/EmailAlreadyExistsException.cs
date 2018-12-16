namespace Desafio.Domain.Exceptions
{
    public class EmailAlreadyExistsException : DesafioException
    {
        public EmailAlreadyExistsException() : base("email_already_exists_exception", 1)
        {
        }
    }
}
