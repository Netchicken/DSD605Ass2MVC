using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DSD605Ass2MVC.Pages.ClaimsManager
{
    public class IndexModel : PageModel
    {
        //import the userManger and generate a list of users
        public UserManager<IdentityUser> UserManager { get; set; }
        public IndexModel(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }
        public List<IdentityUser> Users { get; set; }
        public async Task OnGetAsync()
        {
            Users = await UserManager.Users.ToListAsync();
        }

    }
}
