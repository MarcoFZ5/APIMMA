using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIMMA.Validations
{
    public class ValidationFilter<T> : IAsyncActionFilter
    {
        private readonly IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dto = context.ActionArguments.Values.OfType<T>().FirstOrDefault();

            if (dto == null)
            {
                await next();
                return;
            }

            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                context.Result = new BadRequestObjectResult(result.Errors);
                return;
            }

            await next();
        }
    }
}
