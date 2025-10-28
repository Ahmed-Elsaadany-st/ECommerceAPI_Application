using Domain.Exceptions;
using Shared.ErrorModels;

namespace ECommerce.CustomeMiddlewares
{
    public class CustomeExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeExceptionHandlerMiddleware> _logger;

        public CustomeExceptionHandlerMiddleware(RequestDelegate Next,ILogger<CustomeExceptionHandlerMiddleware>logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                //--------------------Set Status Code for Response
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var Response = new ErrorToReturn()
            {
                ErrorMessage= ex.Message
            };
            Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthrizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException,Response),
                 _ => StatusCodes.Status500InternalServerError
            };



            //---------------------Set Content Type for Response
            httpContext.Response.ContentType = "application/json";
            #region Deleted
            //---------------------Response object
            //var Response = new ErrorToReturn()
            //{
            //    StatusCode = httpContext.Response.StatusCode,
            //    ErrorMessage = ex.Message
            //}; 
            #endregion
            //-------------------Return Object As Json
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The End Point {httpContext.Request.Path} is not found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
