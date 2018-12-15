using Desafio.Business.Dtos;

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
 
        }
    }
}
