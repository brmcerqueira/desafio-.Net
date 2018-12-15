using Desafio.Domain.Exceptions;
using Desafio.Web.Resources;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Desafio.Web
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var code = HttpStatusCode.InternalServerError;
                object result = null;

                if (exception is DesafioException)
                {
                    var desafioException = exception as DesafioException;
                    result = new
                    {
                        message = Resource.ResourceManager.GetString(desafioException.Message),
                        errorCode = desafioException.Code
                    };
                }
                else
                {
                    result = new
                    {
                        message = exception.Message,
                        errorCode = 0
                    };
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
