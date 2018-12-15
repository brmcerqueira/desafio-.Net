using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.Web.Test
{
    public class DefaultControllerTest
    {
        private readonly TestServer server;

        public DefaultControllerTest()
        {
            server = new TestServer(new WebHostBuilder()
                .UseLightInject()
                .UseStartup<Startup>());            
        }

        [Fact]
        public async Task SignUp()
        {
            var response = await server.CreateRequest("signup").Json(new
            {
                firstName = "Hello",
                lastName = "World",
                email = "hello@world.com",
                password = "hunter2",
                phones = new object[] {
                    new {
                        number = 988887888,
                        area_code = 81,
                        country_code = "+55"
                    }
                }
            }).PostAsync();
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task SignIn()
        {
            var response = await server.CreateRequest("signin").Json(new
            {
                email = "brmcerqueira@gmail.com",
                password = "123456"
            }).PostAsync();
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert.Equal("Hello World!", responseString);
        }

        [Fact]
        public async Task Me()
        {
            var response = await server.CreateRequest("me").GetAsync();
            response.EnsureSuccessStatusCode();
        }
    }
}
