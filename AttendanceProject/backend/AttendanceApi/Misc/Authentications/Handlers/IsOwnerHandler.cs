using System.IO.Pipelines;
using System.Security.Claims;
using AttendanceApi.Interfaces;
using AttendanceApi.Misc.Authentications.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace AttendanceApi.Misc.Authentications.Handlers;

public class IsOwnerHandler : AuthorizationHandler<IsOwnerRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOwnerService _ownerService;
    private readonly ILogger<IsOwnerHandler> _logger;
    public IsOwnerHandler(IHttpContextAccessor httpContextAccessor, IOwnerService ownerService, ILogger<IsOwnerHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _ownerService = ownerService;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerRequirement requirement)
    {
        var username = context.User.FindFirst(ClaimTypes.Name)?.Value;

        var pathSegments = _httpContextAccessor.HttpContext?.Request.Path.Value?.Trim('/').Split('/');
        if (pathSegments == null || pathSegments.Length < 2)
        {
            context.Fail();
            return;
        }

        var reference = Array.IndexOf(pathSegments, "api");

        var resourceType = pathSegments[reference+2];
        
        int resourceId;
        if (!int.TryParse(pathSegments[reference+3], out resourceId))
        {
            context.Fail();
            return;
        }

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(resourceType))
        {
            context.Fail();
            return;
        }

        var isOwner = await _ownerService.IsOwnerOfResource(username, resourceType, resourceId);

        if (isOwner)
        {
            context.Succeed(requirement);
            return;
        }

        context.Fail();
        return;
    }
}