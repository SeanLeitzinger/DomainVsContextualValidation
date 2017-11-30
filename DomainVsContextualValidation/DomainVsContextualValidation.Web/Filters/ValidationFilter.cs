using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace DomainVsContextualValidation.Web.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var modelState = context.ModelState;

            if (!modelState.IsValid)
            {
                var errors = modelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(errors);
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }

}
