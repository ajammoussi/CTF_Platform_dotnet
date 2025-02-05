using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidatePaginationAttribute : ActionFilterAttribute
{
    private readonly int _maxPageSize;
    public ValidatePaginationAttribute(int maxPageSize = 30)
    {
        _maxPageSize = maxPageSize;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("pageNumber", out var pageNumberObj) &&
            context.ActionArguments.TryGetValue("pageSize", out var pageSizeObj))
        {
            if (pageNumberObj is int pageNumber && pageNumber <= 0)
            {
                context.Result = new BadRequestObjectResult("Page number must be greater than 0.");
                return;
            }

            if (pageSizeObj is int pageSize && (pageSize <= 0 || pageSize > _maxPageSize))
            {
                context.Result = new BadRequestObjectResult($"Page size must be between 1 and {_maxPageSize}.");
                return;
            }
        }
        base.OnActionExecuting(context);
    }
}
