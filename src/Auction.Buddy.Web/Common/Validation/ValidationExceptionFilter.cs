using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auction.Buddy.Web.Common.Validation
{
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ValidationException exception)
                HandleValidationException(context, exception);
            return base.OnExceptionAsync(context);
        }

        private void HandleValidationException(ExceptionContext context, ValidationException exception)
        {
            context.ExceptionHandled = true;
            context.Result = new BadRequestObjectResult(exception.Errors);
        }
    }
}