using System.Net;

namespace Desafio.Web.Test
{
    public class ResponseExpected
    {
        public ResponseExpected(HttpStatusCode statusCode, int? errorCode = null)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public HttpStatusCode StatusCode { get; }
        public int? ErrorCode { get; }
    }
}
