using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InverviewProject.Middlewares
{
    public class StrangeMiddleware
    {
        private readonly RequestDelegate _next;

        public StrangeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value.Contains("WeatherForecast", StringComparison.OrdinalIgnoreCase)) {
                    await httpContext.Response.WriteAsync("T:h:i:s_i:s_n:o:t_c:o:r:r:e:c:t_r:e:s:u:l:t".Replace('_', ' ').Replace(":", ""));
                    return;
                }

            await _next.Invoke(httpContext);
        }
    }

    public static class StrangeMiddlewareExtensions
    {
        public static IApplicationBuilder UseStrangeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StrangeMiddleware>();
        }
    }
}
