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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                //--------------------Set Status Code for Response
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };



                //---------------------Set Content Type for Response
                httpContext.Response.ContentType= "application/json";
                //---------------------Response object
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };
                //-------------------Return Object As Json
                await httpContext.Response.WriteAsJsonAsync(Response);

               
            }
        }

    }
}
