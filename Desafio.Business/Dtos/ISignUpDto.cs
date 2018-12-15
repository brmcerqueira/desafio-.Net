namespace Desafio.Business.Dtos
{
    public interface ISignUpDto
    {
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        string Password { get; }
        IPhoneDto[] Phones { get; }
    }
}