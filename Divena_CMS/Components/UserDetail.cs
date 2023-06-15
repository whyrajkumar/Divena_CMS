using Divena_CMS.Models;
using Divena_CMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Divena_CMS.Components
{
    public class UserDetail : ViewComponent
    {
        private UserManager<AppUser> userManager;
        public UserDetail(UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userName = HttpContext.User.Identity.Name;
            return View(await GetUser());
            //return View();
        }

        public async Task<LoggedUser> GetUser()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            var roles = await userManager.GetRolesAsync(user);

            LoggedUser loggedUser = new LoggedUser();
            loggedUser.UserName = user.UserName;
            loggedUser.Role = roles.FirstOrDefault();

            return loggedUser;
        }
    }
}
