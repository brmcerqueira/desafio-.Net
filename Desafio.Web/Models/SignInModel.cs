using Desafio.Business.Dtos;

namespace Desafio.Web.Models
{
    public class SignInModel : ISignInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
