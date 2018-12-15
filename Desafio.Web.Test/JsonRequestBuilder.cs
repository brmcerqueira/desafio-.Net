using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Desafio.Web.Test
{
    public static class JsonRequestBuilder
    {
        public static RequestBuilder Json(this RequestBuilder builder, object content)
        {
            return builder.And(m =>
            {
                m.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            });
        }
    }
}
