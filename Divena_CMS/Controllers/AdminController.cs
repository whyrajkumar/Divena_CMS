using Divena_CMS.Models;
using Divena_CMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Divena_CMS.Controllers
{
    [Authorize(Roles = "Admin,Subadmin,Client")]
    public class AdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AdminController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMrg, SignInManager<AppUser> signMgr)
        {
            roleManager = roleMgr;
            userManager = userMrg;
            signInManager = signMgr;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Role()
        {
            ViewBag.Title = "All Roles";
            return View(roleManager.Roles);
        }

        public IActionResult CreateRole()
        {
            ViewBag.Title = "All Roles";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Role");
                else
                    AddErrorsFromResult(result);
            }
            return View(name);
        }

        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }


        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            AddErrorsFromResult(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            AddErrorsFromResult(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Role");
                else
                    AddErrorsFromResult(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Role", roleManager.Roles);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }


    }
}
