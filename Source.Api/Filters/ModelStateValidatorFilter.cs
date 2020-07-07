using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Source.Api.Extensions;
using Source.Domain.Shared;

namespace Source.Api.Filters
{
    public class ModelStateValidatorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errors = new ErrorViewModel(context.ModelState.GetErrorMessages());
                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }
}