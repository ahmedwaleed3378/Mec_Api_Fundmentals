// Core/Middleware/ErrorHandlingMiddleware.cs
using System.Net;
using System.Text.Json;
using Mec_Api_Fundmentals.Models;
using Mec_Api_Fundmentals.Models.Exceptions;

namespace Mec_Api_Fundmentals.Core.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var errorResponse = error switch
                {
                    ApiException e => 
                        (e.StatusCode, ApiResponse<object>.CreateError(e.Message)),
                    KeyNotFoundException e => 
                        (StatusCodes.Status404NotFound, ApiResponse<object>.CreateError("Not found")),
                    _ => (StatusCodes.Status500InternalServerError, 
                         ApiResponse<object>.CreateError("An internal server error occurred."))
                };

                response.StatusCode = errorResponse.Item1;
                await response.WriteAsJsonAsync(errorResponse.Item2);
            }
        }
    }
}
