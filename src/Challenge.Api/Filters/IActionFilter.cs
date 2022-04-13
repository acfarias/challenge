using Challenge.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Challenge.Api.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly NotificationContext _notificationContext;

        public ActionFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                var errorMessage = context.Exception?.Message ?? context.Exception?.InnerException?.Message;
                context.ExceptionHandled = true;
                _notificationContext.AddNotification(string.Empty, errorMessage);

                context.Result = new BadRequestObjectResult(errorMessage)
                {
                    StatusCode = context.HttpContext.Response.StatusCode,
                    Value = _notificationContext.Notifications()
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
