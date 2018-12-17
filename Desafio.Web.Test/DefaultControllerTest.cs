using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Desafio.Web.Test
{
    public class DefaultControllerTest
    {
        private const int EMAIL_ALREADY_EXISTS_EXCEPTION = 1;
        private const int AUTHENTICATION_EXCEPTION = 2;
        private const int VALIDATION_EXCEPTION = 3;
        
        private readonly TestServer server;
        private readonly ITestOutputHelper output;

        public DefaultControllerTest(ITestOutputHelper output)
        {
            server = new TestServer(new WebHostBuilder()
                 .UseLightInject()
                 .UseStartup<Startup>());
            this.output = output;
        }

        public static IEnumerable<object[]> SignUpData()
        {
            var email = TestHelpers.GenerateRandomEmail();
            return new List<object[]>
            {
                new object[] {
                    CreateSignUpInput(email),
                    new ResponseExpected(HttpStatusCode.OK)
                },
                new object[] {
                    CreateSignUpInput(email),
                    new ResponseExpected(HttpStatusCode.InternalServerError, EMAIL_ALREADY_EXISTS_EXCEPTION)
                },
            };
        }

        private static object CreateSignUpInput(string email = null)
        {
            return new
            {
                firstName = "Hello",
                lastName = "World",
                email = email ?? TestHelpers.GenerateRandomEmail(),
                password = "hunter2",
                phones = new object[] {
                        new {
                            number = 988887888,
                            area_code = 81,
                            country_code = "+55"
                        }
                    }
            };
        }

        [Theory]
        [MemberData(nameof(SignUpData))]
        public async Task SignUp(object content, ResponseExpected responseExpected)
        {
            await SignUpRequest(content, responseExpected);
        }

        private async Task SignUpRequest(object content, ResponseExpected responseExpected)
        {
            output.WriteLine($"Fazendo um post para a rota 'signup' com o seguinte body '{JsonConvert.SerializeObject(content)}'");
            await server.CreateRequest("signup").Json(content).PostAsync().CheckResponse(output, responseExpected);
        }

        public static IEnumerable<object[]> SignInData()
        {
            var emailOne = TestHelpers.GenerateRandomEmail();
            var emailTwo = TestHelpers.GenerateRandomEmail();
            var emailThree = TestHelpers.GenerateRandomEmail();

            return new List<object[]>
            {
                new object[] {
                   emailOne,
                   new
                   {
                       email = emailOne,
                       password = "hunter2"
                   },
                   new ResponseExpected(HttpStatusCode.OK)
                },
                new object[] {
                   emailTwo,
                   new
                   {
                       email = emailTwo.Substring(1),
                       password = "hunter2"
                   },
                   new ResponseExpected(HttpStatusCode.InternalServerError, AUTHENTICATION_EXCEPTION)
                },
                new object[] {
                   emailThree,
                   new
                   {
                       email = emailThree,
                       password = "hunter3"
                   },
                   new ResponseExpected(HttpStatusCode.InternalServerError, AUTHENTICATION_EXCEPTION)
                },
            };
        }
        
        [Theory]
        [MemberData(nameof(SignInData))]
        public async Task SignIn(string email, object content, ResponseExpected responseExpected)
        {
            await SignUpRequest(CreateSignUpInput(email), new ResponseExpected(HttpStatusCode.OK));

            await SignInRequest(content, responseExpected);
        }

        private async Task<string> SignInRequest(object content, ResponseExpected responseExpected)
        {
            output.WriteLine($"Fazendo um post para a rota 'signin' com o seguinte body '{JsonConvert.SerializeObject(content)}'");
            return await server.CreateRequest("signin").Json(content).PostAsync().CheckResponse(output, responseExpected);
        }

        public static IEnumerable<object[]> MeData => new List<object[]>
        {
            new object[] {
                TestHelpers.GenerateRandomEmail(),
                new Func<string, string>(t => t),
                new ResponseExpected(HttpStatusCode.OK)
            },
            new object[] {
                TestHelpers.GenerateRandomEmail(),
                new Func<string, string>(t => t.Substring(1)),
                new ResponseExpected(HttpStatusCode.Unauthorized)
            },
            new object[] {
                TestHelpers.GenerateRandomEmail(),
                new Func<string, string>(t => ""),
                new ResponseExpected(HttpStatusCode.Unauthorized)
            },
        };

        [Theory]
        [MemberData(nameof(MeData))]
        public async Task Me(string email, Func<string, string> transformAuthorization, ResponseExpected responseExpected)
        {
            dynamic content = CreateSignUpInput(email);

            await SignUpRequest(content, new ResponseExpected(HttpStatusCode.OK));

            var signIn = new
            {
                content.email,
                content.password
            };

            var authorization = await SignInRequest(signIn, new ResponseExpected(HttpStatusCode.OK));

            await MeRequest(transformAuthorization(authorization), responseExpected);
        }

        private async Task MeRequest(string authorization, ResponseExpected responseExpected)
        {
            output.WriteLine($"Fazendo um get para a rota 'me' com a autorização '{authorization}'");
            await server.CreateRequest("me").AddHeader("Authorization", $"Bearer {authorization}").GetAsync().CheckResponse(output, responseExpected);
        }
    }
}
