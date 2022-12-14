using Microsoft.AspNetCore.Authorization;

namespace DSD605Ass2MVC.AuthorizationRequirements
{
    //IAuthorizationRequirement is a marker service with no methods, and the mechanism for tracking whether authorization is successful.

    //this class is imported into the   public class HasClaimHandler : AuthorizationHandler<ViewRolesRequirement>

    public class ViewRolesRequirement : IAuthorizationRequirement
    {
        public int Months { get; }

        //The constructor takes an int as a parameter and ensures that it is NOT a positive number 
        public ViewRolesRequirement(int months)
        {
            Months = months > 0 ? 0 : months;
        }

    };

    //   return Task.CompletedTask;


    //The HandleAsync method is implemented as required by the IAuthorizationHandler interface we extract this out in the next lesson
    //    public Task HandleAsync(AuthorizationHandlerContext context)
    //    {
    //        //  The user is checked to see if they have a Joining Date claim.If not, the handler is exited
    //        var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
    //        if (joiningDateClaim == null)
    //        {
    //            return Task.CompletedTask;
    //        }
    //        // The joining date is assessed to see if it exists and if its value is older than the age passed in. 
    //        var joiningDate = Convert.ToDateTime(joiningDateClaim);

    //        if (context.User.HasClaim("Permission", "View Roles") && joiningDate > DateTime.MinValue &&
    //            joiningDate < DateTime.Now.AddMonths(Months))
    //        {
    //            context.Succeed(this);
    //        }

    //        // If the requirement is not satisfied, Task.CompletedTask is returned to satisfy the HandleAsync method signature
    //        return Task.CompletedTask;
    //    }
    //}

}
