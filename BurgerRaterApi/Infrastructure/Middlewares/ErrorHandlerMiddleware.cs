using Microsoft.AspNetCore.Http;

using System;
using System.Threading.Tasks;

namespace BurgerRaterApi.Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api-middleware", out _, out var remaining))
            {
                if (remaining.StartsWithSegments("/error"))
                    throw new Exception("This is an exception thrown from middleware.");

                if (remaining.StartsWithSegments("/status", out _, out remaining))
                {
                    var statusCodeString = remaining.Value.Trim('/');

                    if (int.TryParse(statusCodeString, out var statusCode))
                    {
                        context.Response.StatusCode = statusCode;
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
