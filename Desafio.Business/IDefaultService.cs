using Desafio.Business.Dtos;

namespace Desafio.Business
{
    public interface IDefaultService
    {
        void SignUp(ISignUpDto dto);
        string SignIn(ISignInDto dto);
        object Me(int userId);
    }
}