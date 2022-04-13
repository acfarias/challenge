using Challenge.Domain.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Challenge.Api.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications())
            {
                await SetContextResultAsync(context);
                return;
            }
            else if (!context.ModelState.IsValid)
            {
                var modelErrors = context.ModelState.Values.SelectMany(er => er.Errors).ToList();
                foreach (var err in modelErrors)
                    _notificationContext.AddNotification(string.Empty, err.ErrorMessage);

                context.ModelState.Clear();
                await SetContextResultAsync(context);
                return;
            }

            await next();
        }

        private async Task SetContextResultAsync(ResultExecutingContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications());
            await context.HttpContext.Response.WriteAsync(notifications);
        }
    }
}
