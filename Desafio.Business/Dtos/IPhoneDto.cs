namespace Desafio.Business.Dtos
{
    public interface IPhoneDto
    {
        int AreaCode { get; }
        string CountryCode { get; }
        int Number { get; }
    }
}