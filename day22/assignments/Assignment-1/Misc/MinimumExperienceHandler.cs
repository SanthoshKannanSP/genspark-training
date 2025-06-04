using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace assignment_1.Misc
{
    public class MinimumExperienceHandler : AuthorizationHandler<MinimumExperienceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MinimumExperienceRequirement requirement)
        {
            var experienceClaim = context.User.FindFirst("YearsOfExperience");

            if (experienceClaim == null)
                return Task.CompletedTask;

            int years;
            if (!int.TryParse(experienceClaim.Value, out years))
                return Task.CompletedTask;

            if (years >= requirement.MinimumExperience)
                    context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}