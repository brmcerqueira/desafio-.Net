using Desafio.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
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

        public async Task Invoke(HttpContext context, IStringLocalizer stringLocalizer)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var code = HttpStatusCode.InternalServerError;
                object result = null;

                if (exception is ValidationException)
                {
                    var validationException = exception as ValidationException;
                    result = new
                    {
                        Message = stringLocalizer[validationException.Message].Value,
                        ErrorCode = validationException.Code,
                        validationException.Errors
                    };
                }
                else if(exception is DesafioException)
                {
                    var desafioException = exception as DesafioException;
                    result = new
                    {
                        Message = stringLocalizer[desafioException.Message].Value,
                        ErrorCode = desafioException.Code
                    };
                }
                else
                {
                    result = new
                    {
                        Message = stringLocalizer["SystemUnavailable"],
                        ErrorCode = 0
                    };
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
