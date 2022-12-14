using DSD605Ass2MVC.AuthorizationRequirements;

using Microsoft.AspNetCore.Authorization;

namespace DSD605Ass2MVC.AuthorizationHandlers
{
    //this is a seperate class that inherits from AuthorizationHandler 
    public class IsInRoleHandler : AuthorizationHandler<ViewRolesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ViewRolesRequirement req)
        {
            if (context.User.IsInRole("Admin")) //does the user have the role Admin?
            {
                context.Succeed(req);
            }
            return Task.CompletedTask;
        }
    }

}
