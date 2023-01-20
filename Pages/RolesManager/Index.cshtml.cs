using DSD605Ass2MVC.Data;
using DSD605Ass2MVC.DTO;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSD605Ass2MVC.Pages.RolesManager
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly UserManager<IdentityUser> _userManager;
        public IndexModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }
        //declare a public List<IdentityRole> property and use the RoleManager service to populate it with all our existing roles. 
        public List<IdentityRole> Roles { get; set; }
        public List<IdentityUser> Users { get; set; }
        public List<UserRolesDTO> UserAndRoles { get; set; }

        public List<UserRolesDTO> GetUserAndRoles()
        {
            var list = (from user in _context.Users
                        join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                        join role in _context.Roles on userRoles.RoleId equals role.Id
                        select new UserRolesDTO { UserName = user.UserName, RoleName = role.Name }).ToList();
            return list;
        }

        public void OnGet()
        {
            Roles = _roleManager.Roles.ToList();
            Users = _userManager.Users.ToList();
            UserAndRoles = GetUserAndRoles();

        }
    }

}

