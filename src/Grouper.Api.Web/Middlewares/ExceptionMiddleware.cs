using Grouper.Api.Infrastructure.DTOs.Outbound;
using Grouper.Api.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Middlewares
{
    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ExceptionMiddleware
    {
        private static readonly string NewLine = Environment.NewLine;

        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.Clear();

                var guid = Guid.NewGuid().ToString("N");
                var token = context.TraceIdentifier;

                var json = new ErrorModel();

                if (ex is ApiException exception)
                {
                    json.Title = $"{ex.Message} Error reference {guid} token {token}";
                    json.Status = (int)exception.Code;
                    json.Fields = exception.Fields;
                }
                else
                {
                    json.Title = $"An error occurred. Try it again later. Error reference {guid} token {token}";
                    json.Status = (int)HttpStatusCode.InternalServerError;
                }

                json.Detail = ex.ToString();

                var code = json.Status;
                context.Response.StatusCode = code;
                context.Response.ContentType = "application/json";

                var r = context.Request;
                var headers = String.Join(NewLine, r.Headers.Select(x => $"{x.Key}:{x.Value}"));

                // log error, after adding logger
                //logger.Error(ex,
                //    $"[{guid}] Failed HTTP {r.Method} request {r.Path} => {code} {NewLine} {r.QueryString} {NewLine} {headers} {NewLine} token {token} {NewLine}"
                //    );

                await context.Response.WriteAsync(JsonSerializer.Serialize(json));
            }
        }
    }
}
