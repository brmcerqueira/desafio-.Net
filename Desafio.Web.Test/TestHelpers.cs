using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xbehave;
using Xunit;

namespace Desafio.Web.Test
{
    public static class TestHelpers
    {
        public static RequestBuilder Json(this RequestBuilder builder, object content)
        {
            return builder.And(m =>
            {
                m.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            });
        }

        public static async Task CheckResponse(this Task<HttpResponseMessage> taskResponse, ResponseExpected responseExpected)
        {
            var response = await taskResponse;
            var responseData = await response.Content.ReadAsStringAsync();

            $"Dados da resposta: '{response.StatusCode}' - '{response.ReasonPhrase}'"
                .x(() => { });

            $"Verificando se o status é '{(int)responseExpected.StatusCode}'"
                .x(() => 
                {
                    Assert.Equal(responseExpected.StatusCode, response.StatusCode);
                    if (responseExpected.ErrorCode.HasValue)
                    {
                        var messageError = JsonConvert.DeserializeObject<MessageError>(responseData);

                        $"Dados do erro: '{messageError.ErrorCode}' - '{messageError.Message}'"
                              .x(() => { });

                        $"Verificando se o codigo de erro é '{responseExpected.ErrorCode.Value}'"
                            .x(() => Assert.Equal(responseExpected.ErrorCode.Value, messageError.ErrorCode));
                    }
                });
        }
    }
}
