using Microsoft.AspNetCore.Authorization;

namespace DSD605Ass2MVC.AuthorizationRequirements
{

    public class ViewRolesRequirement : IAuthorizationRequirement, IAuthorizationHandler
    {
        public int Months { get; } = -6;

        //The constructor takes an int as a parameter and ensures that it is NOT a positive number 
        public ViewRolesRequirement()
        {
            // Months = months > 0 ? 0 : months;
        }


        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            //  The user is checked to see if they have a Joining Date claim.If not, the handler is exited
            var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
            if (joiningDateClaim == null)
            {
                return Task.CompletedTask;
            }
            // The joining date is assessed to see if it exists and if its value is older than the age passed in. 
            var joiningDate = Convert.ToDateTime(joiningDateClaim);

            if (context.User.HasClaim("Permission", "View Roles") && joiningDate > DateTime.MinValue &&
                joiningDate < DateTime.Now.AddMonths(Months))
            {
                context.Succeed(this);
            }

            // If the requirement is not satisfied, Task.CompletedTask is returned to satisfy the HandleAsync method signature
            return Task.CompletedTask;
        }
    }

};
