using DSD605Ass2MVC.AuthorizationRequirements;

using Microsoft.AspNetCore.Authorization;

namespace DSD605Ass2MVC.AuthorizationHandlers
{
    //Each IAuthorizationHandler is responsible for checking if requirements are met:
    public class HasClaimHandler : AuthorizationHandler<ViewRolesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewRolesRequirement req)
        {
            //pass in the current user and look for their joining date value
            var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
            if (joiningDateClaim == null) //if there isn't one then return
            {
                return Task.CompletedTask;
            }
            // if there is a joining date then check the date and see if it is less than 6 months and if the person has the persmission to see roles
            var joiningDate = Convert.ToDateTime(joiningDateClaim);
            //

            if (joiningDate < DateTime.Now.AddMonths(req.Months))
            {
                Console.Write("HasClaimHandler date" + joiningDate);

                if (context.User.HasClaim("Permission", "View Roles"))
                {
                    Console.Write("HasClaimHandler Permission" + context.User.HasClaim("Permission", "View Roles"));
                    context.Succeed(req); //they have the permissions
                }


            }


            //if the date is greater than 6 months and they have the claim to View Roles then return suceed for that reqirement 

            return Task.CompletedTask;
        }
    }
}
