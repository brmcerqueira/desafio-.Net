namespace Desafio.Domain.Exceptions
{
    public class EmailAlreadyExistsException : DesafioException
    {
        public EmailAlreadyExistsException() : base("EmailAlreadyExistsException", 1)
        {
        }
    }
}
