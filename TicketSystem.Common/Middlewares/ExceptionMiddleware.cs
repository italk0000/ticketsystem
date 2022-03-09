using System.Text.Json;
using Microsoft.AspNetCore.Http;
using TicketSystem.Common.Exceptions;
using TicketSystem.Common.Models.Response;

namespace TicketSystem.Common.Middlewares
{
    /// <summary>
    /// 統一處理例外
    /// </summary>
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (HttpCodeException ex)
            {
                context.Response.StatusCode = ex.StatusCodes;
            }
            catch (Exception ex)
            {
                var statusCode = ex switch
                {
                    MessageException _ => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                var message = ex switch
                {
                    MessageException exception => exception.Message,
                    _ => ex.ToString(),
                };

                var apiResponse = new ApiResponse(statusCode, message: message);
                context.Response.StatusCode = StatusCodes.Status200OK;
                await context.Response.WriteAsync(SerializerResponse(apiResponse));
            }
        }

        private string SerializerResponse(ApiResponse apiResponse)
        {
            return JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
        }
    }
}
