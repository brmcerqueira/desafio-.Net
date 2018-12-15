using Desafio.Business.Dtos;

namespace Desafio.Web.Models
{
    public class SignUpModel : ISignUpDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PhoneModel[] Phones { get; set; }
        IPhoneDto[] ISignUpDto.Phones { get => Phones; }
    }
}
