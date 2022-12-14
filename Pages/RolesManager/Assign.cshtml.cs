using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace DSD605Ass2MVC.Pages.RolesManager
{
   // [Authorize(Roles = "Admin")]
    public class AssignModel : PageModel
    {
        //We inject the UserManager and RoleManager services into the PageModel class 
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AssignModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public SelectList Roles { get; set; }
        public SelectList Users { get; set; }

        [BindProperty, Required, Display(Name = "Role")]
        public string SelectedRole { get; set; }
        [BindProperty, Required, Display(Name = "User")]
        public string SelectedUser { get; set; }

        public async Task OnGet()
        {
            await GetOptions();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {//We get the user with the selected name and assign the selected user to the selected role 
                var user = await _userManager.FindByNameAsync(SelectedUser);
                await _userManager.AddToRoleAsync(user, SelectedRole);
                return RedirectToPage("/RolesManager/Index");
            }
            await GetOptions(); return Page();
        }

        //We declare a private method that assign users and roles to SelectList object 
        public async Task GetOptions()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            Roles = new SelectList(roles, nameof(IdentityRole.Name));
            Users = new SelectList(users, nameof(IdentityUser.UserName));
        }
    }

}

