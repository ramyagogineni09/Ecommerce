

using System.Net;
using System.Text.Json;


namespace Ecommerce.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ExceptionMiddleware(RequestDelegate next) {


            _next = next;
            

        }

        public async Task InvokeAsync(HttpContext context) {
            try
            {
                await _next(context);

            }
            catch (Exception ex) {
               

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var responce = new {

                    Message = "Something went wrong please try again later",
                    Detailed = ex.Message,
                    status=context.Response.StatusCode

                
                };

                
                await context.Response.WriteAsync(JsonSerializer.Serialize(responce));
            }
    }
    }
}
