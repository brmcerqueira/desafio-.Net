using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

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

        public static async Task CheckResponse(this Task<HttpResponseMessage> taskResponse, ITestOutputHelper output, ResponseExpected responseExpected)
        {
            var response = await taskResponse;
            var responseData = await response.Content.ReadAsStringAsync();

            output.WriteLine($"Dados da resposta: '{response.StatusCode}' - '{response.ReasonPhrase}' - {responseData}");

            output.WriteLine($"Verificando se o status é '{(int)responseExpected.StatusCode}'");

            Assert.Equal(responseExpected.StatusCode, response.StatusCode);

            if (responseExpected.ErrorCode.HasValue)
            {
                var messageError = JsonConvert.DeserializeObject<MessageError>(responseData);

                output.WriteLine($"Dados do erro: '{messageError.ErrorCode}' - '{messageError.Message}'");

                output.WriteLine($"Verificando se o codigo de erro é '{responseExpected.ErrorCode.Value}'");

                Assert.Equal(responseExpected.ErrorCode.Value, messageError.ErrorCode);
            }
        }
    }
}
