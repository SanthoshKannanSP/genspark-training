using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AttendanceApi.Misc;

public class LogRequestFilter : ActionFilterAttribute
{
    private static readonly ILog log = LogManager.GetLogger(typeof(LogRequestFilter));

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var timestamp = DateTime.Now.ToString("o");
        var username = context.HttpContext.User?.Identity?.Name ?? "Unknown";
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        var method = context.HttpContext.Request.Method;

        log.Info($"[Server Called]\nTimestamp: {timestamp}\nUser: {username}\nEndpoint: {controller}/{action}\nMethod: {method}");
    }
}