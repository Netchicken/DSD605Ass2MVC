using DSD605Ass2MVC.DTO;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSD605Ass2MVC.Controllers
{
    public class ClaimsManagerController : Controller
    {
        //import the userManger and generate a list of users
        public List<IdentityUser> Users { get; set; }

        public UserManager<IdentityUser>? UserManager { get; set; }

        public ClaimsManagerController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }


        // GET: ClaimsManagerController
        public async Task<ActionResult> IndexAsync()
        {

            Users = await UserManager.Users.ToListAsync();

            List<ClaimDTO> claimDTO = new List<ClaimDTO>();
            foreach (var user in Users)
            {
                var claims = await UserManager.GetClaimsAsync(user);

                if (claims.Any())
                {

                    foreach (var claim in claims)
                    {
                        claimDTO.Add(new ClaimDTO(user.UserName, claim.Type, claim.Value, claim.Issuer));

                    }

                }
            }

            return View(claimDTO);
        }

        // GET: ClaimsManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClaimsManagerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClaimsManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClaimsManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClaimsManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClaimsManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClaimsManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
