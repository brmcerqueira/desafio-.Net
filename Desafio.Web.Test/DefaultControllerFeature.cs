using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using Xbehave;
using Xunit;

namespace Desafio.Web.Test
{
    public class DefaultControllerFeature
    {
        private TestServer server;

        [Background]
        public void Background()
        {
            "Criando um servidor de teste"
                .x(() => server = new TestServer(new WebHostBuilder()
                .UseLightInject()
                .UseStartup<Startup>()));
        }

        public static IEnumerable<object[]> SignUpData => new List<object[]>
        {
            new object[] {
                new
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
                }
            },
        };

        [Scenario]
        [MemberData(nameof(SignUpData))]
        public void SignUp(object content, HttpResponseMessage response)
        {
            $"Fazendo um post para a rota 'signup' com o seguinte body '{JsonConvert.SerializeObject(content)}'"
                .x(async () => response = await server.CreateRequest("signup").Json(content).PostAsync());

            "Garantindo o sucesso da requisição"
                .x(() => response.EnsureSuccessStatusCode());
        }

        public static IEnumerable<object[]> SignInData => new List<object[]>
        {
            new object[] {
               new
               {
                   email = "hello@world.com",
                   password = "hunter2"
               }
            },
        };

        [Scenario]
        [MemberData(nameof(SignInData))]
        public void SignIn(object content, HttpResponseMessage response)
        {
            $"Fazendo um post para a rota 'signin' com o seguinte body '{JsonConvert.SerializeObject(content)}'"
                .x(async () => response = await server.CreateRequest("signin").Json(content).PostAsync());

            "Garantindo o sucesso da requisição"
                .x(() => response.EnsureSuccessStatusCode());

            //var responseString = await response.Content.ReadAsStringAsync();

            //Assert.Equal("Hello World!", responseString);
        }

        [Scenario]
        public void Me(HttpResponseMessage response)
        {
            "Fazendo um get para a rota 'me'"
                .x(async () => response = await server.CreateRequest("me").GetAsync());

            "Garantindo o sucesso da requisição"
                .x(() => response.EnsureSuccessStatusCode());
        }
    }
}
