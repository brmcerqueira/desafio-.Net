namespace Desafio.Domain
{
    public class UserPhone
    {
        public int Id { get; set; }
        public int AreaCode { get; set; }
        public string CountryCode { get; set; }
        public int Number { get; set; }
        public virtual User User { get; set; }
    }
}
