using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GBC_Travel_Group_42.Areas.BookingManagement.Models;

namespace GBC_Travel_Group_42.Components
{
    public class UserRoleVIewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleVIewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new
            {
                IsSuperAdmin = user != null && await _userManager.IsInRoleAsync(user, "SuperAdmin"),
                IsAdmin = user != null && await _userManager.IsInRoleAsync(user, "Admin")
            };

            return View(model);
        }
    }
}
