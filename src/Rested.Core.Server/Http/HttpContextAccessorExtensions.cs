using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Rested.Core.Server.Http
{
    public static class HttpContextAccessorExtensions
    {
        public static void AddETagResponseHeader(this IHttpContextAccessor httpContext, byte[] key)
        {
            httpContext.HttpContext!.Response.Headers.Add(HeaderNames.ETag, Convert.ToBase64String(key));
        }
    }
}
