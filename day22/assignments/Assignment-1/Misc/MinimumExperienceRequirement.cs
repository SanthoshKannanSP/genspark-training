using Microsoft.AspNetCore.Authorization;

namespace assignment_1.Misc
{
    public class MinimumExperienceRequirement : IAuthorizationRequirement
    {
        public int MinimumExperience { get; }

        public MinimumExperienceRequirement(int minimumExperience)
        {
            MinimumExperience = minimumExperience;
        }
    }
}