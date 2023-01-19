using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

namespace DSD605Ass2MVC.AuthorizationRequirements
{
    public class ViewAgeRequirement : IAuthorizationRequirement, IAuthorizationHandler
    {
        public int AgeLimit { get; } = 18;
        public ViewAgeRequirement()
        {
            // AgeLimit = age;
        }


        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var dateOfBirthClaim = context.User.FindFirst(
             c => c.Type == ClaimTypes.DateOfBirth);

            if (dateOfBirthClaim is null)
            {
                return Task.CompletedTask;
            }
            //convert to date
            var dateOfBirthUser = Convert.ToDateTime(dateOfBirthClaim.Value);

            //get users age
            int calculatedAgeUser = DateTime.Today.Year - dateOfBirthUser.Year;

            // if the agelimit is less than or equal to the calculated age of the user then they are old enough
            if (AgeLimit <= calculatedAgeUser)
            {
                context.Succeed((IAuthorizationRequirement)this);
            }


            // If the requirement is not satisfied, Task.CompletedTask is returned to satisfy the HandleAsync method signature
            return Task.CompletedTask;
        }
    }
}

