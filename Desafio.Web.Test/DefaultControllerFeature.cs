using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
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
                },
                new ResponseExpected(HttpStatusCode.OK)
            },
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
                },
                new ResponseExpected(HttpStatusCode.InternalServerError, 1)
            },
        };

        [Scenario]
        [MemberData(nameof(SignUpData))]
        public void SignUp(object content, ResponseExpected responseExpected)
        {
            $"Fazendo um post para a rota 'signup' com o seguinte body '{JsonConvert.SerializeObject(content)}'"
                .x(() => server.CreateRequest("signup").Json(content).PostAsync().CheckResponse(responseExpected));
        }

        public static IEnumerable<object[]> SignInData => new List<object[]>
        {
            new object[] {
               new
               {
                   email = "hello@world.com",
                   password = "hunter2"
               },
               new ResponseExpected(HttpStatusCode.OK)
            },
        };

        [Scenario]
        [MemberData(nameof(SignInData))]
        public void SignIn(object content, ResponseExpected responseExpected)
        {
            $"Fazendo um post para a rota 'signin' com o seguinte body '{JsonConvert.SerializeObject(content)}'"
                .x(() => server.CreateRequest("signin").Json(content).PostAsync().CheckResponse(responseExpected));
        }

        public static IEnumerable<object[]> MeData => new List<object[]>
        {
            new object[] {
               "",
               new ResponseExpected(HttpStatusCode.OK)
            },
        };

        [Scenario]
        [MemberData(nameof(MeData))]
        public void Me(string authorization, ResponseExpected responseExpected)
        {
            $"Fazendo um get para a rota 'me' com a autorização '{authorization}'"
                .x(() => server.CreateRequest("me").AddHeader("Authorization", authorization).GetAsync().CheckResponse(responseExpected));
        }
    }
}
