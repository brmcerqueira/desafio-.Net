using Desafio.Business.Dtos;
using Desafio.Domain.Exceptions;

namespace Desafio.Business
{
    internal class DefaultService : IDefaultService
    {
        public object Me()
        {
            return new
            {
                FirstName = ""
            };
        }

        public object SignIn(ISignInDto dto)
        {
            return new
            {
                Token = ""
            };
        }

        public void SignUp(ISignUpDto dto)
        {
            throw new EmailAlreadyExistsException();
        }
    }
}
