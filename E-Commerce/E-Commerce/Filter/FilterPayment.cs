using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce.Filter
{
    public class FilterPayment : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var requestPath = context.HttpContext.Request.Path;

            if (IsValidUrl(requestPath))
            {
                context.Result = new RedirectToActionResult("Index", "Callback", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private bool IsValidUrl(string path)
        {
            return path.StartsWith("/api/Payment");
        }
    }
}
