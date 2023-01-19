using Microsoft.AspNetCore.Authorization;

namespace DSD605Ass2MVC.AuthorizationRequirements
{
    //IAuthorizationRequirement is a marker service with no methods, and the mechanism for tracking whether authorization is successful.
    public class ViewClaimsRequirement : IAuthorizationRequirement
    {
        public int Months { get; } = -6;

        //The constructor takes an int as a parameter and ensures that it is NOT a positive number 
        public ViewClaimsRequirement()
        {
            //  Months = months > 0 ? 0 : months;
        }


    }
}
