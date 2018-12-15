using Desafio.Business.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Desafio.Web.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PhoneModel : IPhoneDto
    {
        public int Number { get; set; }
        public int AreaCode { get; set; }
        public string CountryCode { get; set; }
    }
}
