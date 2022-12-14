using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSD605Ass2MVC.Pages.RolesManager
{
    public class IndexModel : PageModel
    {

        //inject the RoleManager<TRole> service into the IndexModel via its constructor and assign it to a private field for later use 
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        //declare a public List<IdentityRole> property and use the RoleManager service to populate it with all our existing roles. 
        public List<IdentityRole> Roles { get; set; }
        public void OnGet()
        {
            Roles = _roleManager.Roles.ToList();
        }
    }

}

