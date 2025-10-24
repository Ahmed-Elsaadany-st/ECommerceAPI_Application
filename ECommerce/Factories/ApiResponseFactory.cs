using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace ECommerce.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext Context)
        {
          
                var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
                .Select(M => new ValidationErrors()
                {
                    Field = M.Key,
                    Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                });

                var Response = new ValidationErrorToReturn()
                {
                    validationErrors = Errors
                };
                return new BadRequestObjectResult(Response);
          

        }
    }
}
