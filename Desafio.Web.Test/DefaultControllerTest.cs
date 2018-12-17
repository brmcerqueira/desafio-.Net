using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
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
                new ResponseExpected(HttpStatusCode.InternalServerError, EMAIL_ALREADY_EXISTS_EXCEPTION)
            },
        };

        [Theory]
        [MemberData(nameof(SignUpData))]
        public async Task SignUp(object content, ResponseExpected responseExpected)
        {
            output.WriteLine($"Fazendo um post para a rota 'signup' com o seguinte body '{JsonConvert.SerializeObject(content)}'");
            await server.CreateRequest("signup").Json(content).PostAsync().CheckResponse(output, responseExpected);
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
            new object[] {
               new
               {
                   email = "hello2@world.com",
                   password = "hunter2"
               },
               new ResponseExpected(HttpStatusCode.InternalServerError, AUTHENTICATION_EXCEPTION)
            },
            new object[] {
               new
               {
                   email = "hello@world.com",
                   password = "hunter3"
               },
               new ResponseExpected(HttpStatusCode.InternalServerError, AUTHENTICATION_EXCEPTION)
            },
        };

        [Theory]
        [MemberData(nameof(SignInData))]
        public async Task SignIn(object content, ResponseExpected responseExpected)
        {
            output.WriteLine($"Fazendo um post para a rota 'signin' com o seguinte body '{JsonConvert.SerializeObject(content)}'");
            await server.CreateRequest("signin").Json(content).PostAsync().CheckResponse(output, responseExpected);
        }

        public static IEnumerable<object[]> MeData => new List<object[]>
        {
            new object[] {
               "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkhlbGxvIFdvcmxkIiwic3ViIjoiMSIsImVtYWlsIjoiaGVsbG9Ad29ybGQuY29tIiwibmJmIjoxNTQ1MDEwMDczLCJleHAiOjE1NDUyNjkyNzMsImlhdCI6MTU0NTAxMDA3MywiaXNzIjoiRGVzYWZpb0lzc3VlciIsImF1ZCI6IkRlc2FmaW9BdWRpZW5jZSJ9.h7y1ABGQ2kvtOprM3JVigjOIYY6GywOGKZpEutEjA9w",
               new ResponseExpected(HttpStatusCode.OK)
            },
            new object[] {
               "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkhlbGxvIFdvcmxkIiwic3ViIjoiMSIsImVtYWlsIjoiaGVsbG9Ad29ybGQuY29tIiwibmJmIjoxNTQ1MDEyMDkxLCJleHAiOjE1NDUwMTIwOTQsImlhdCI6MTU0NTAxMjA5MSwiaXNzIjoiRGVzYWZpb0lzc3VlciIsImF1ZCI6IkRlc2FmaW9BdWRpZW5jZSJ9.3eA87aoQ-YCjG9lrupRbE2nVGMKFlMVMWrSqbHiu4lc",
               new ResponseExpected(HttpStatusCode.Unauthorized)
            },
            new object[] {
               "",
               new ResponseExpected(HttpStatusCode.Unauthorized)
            },
        };

        [Theory]
        [MemberData(nameof(MeData))]
        public async Task Me(string authorization, ResponseExpected responseExpected)
        {
            output.WriteLine($"Fazendo um get para a rota 'me' com a autorização '{authorization}'");
            await server.CreateRequest("me").AddHeader("Authorization", $"Bearer {authorization}").GetAsync().CheckResponse(output, responseExpected);
        }
    }
}
