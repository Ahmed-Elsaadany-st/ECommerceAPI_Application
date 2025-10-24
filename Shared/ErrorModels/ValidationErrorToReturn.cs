using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorToReturn
    {
        public int StatusCode=(int)HttpStatusCode.BadRequest;
        public string Message { get; set; } = "Validation Failed";
        public IEnumerable<ValidationErrors> validationErrors { get; set; } = [];
    }
}
