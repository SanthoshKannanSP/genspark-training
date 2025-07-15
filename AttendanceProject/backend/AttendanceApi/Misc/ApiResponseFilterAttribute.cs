using AttendanceApi.Models.DTOs;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AttendanceApi.Misc;

public class ApiResponseFilterAttribute : ActionFilterAttribute
{
    private static readonly ILog log = LogManager.GetLogger(typeof(LogRequestFilter));

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            var timestamp = DateTime.Now.ToString("o");
            var username = context.HttpContext.User?.Identity?.Name ?? "Unknown";
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];
            var method = context.HttpContext.Request.Method;
            var exception = context.Exception;
            log.Info($"[Error]\nTimestamp: {timestamp}\nUser: {username}\nEndpoint: {controller}/{action}\nMethod: {method}\nException: {exception.Message}\nStack Trace: {exception.StackTrace}");

            var result = new ApiResponseDTO { Success = false, Data = null, ErrorMessage = context.Exception.Message };
            context.Result = new ObjectResult(result) { StatusCode = 500 };
            context.ExceptionHandled = true;
            return;
        }

        var objectResult = context.Result as ObjectResult;
        if (objectResult != null)
        {
            var result = new ApiResponseDTO { Success = true, Data = objectResult.Value, ErrorMessage = null };
            context.Result = new ObjectResult(result) { StatusCode = objectResult.StatusCode ?? 200 };
        }
        else if (context.Result is EmptyResult)
        {
            var result = new ApiResponseDTO { Success = true, Data = null, ErrorMessage = null };
            context.Result = new ObjectResult(result) { StatusCode = 204 };
        }
    }
}