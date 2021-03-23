using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.web.DTOs;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }

        /*Edit profile*/
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetDataForEditProfileUser(User.Identity.Name));
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(UserPanelViewModel.EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
                return View(profile);
            _userService.EditProfile(User.Identity.Name, profile);
            return Redirect("/login?EditProfile=true");
        }

        //change password 
        [Route("userPanel/changePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("userPanel/changePassword")]
        [HttpPost]
        public IActionResult ChangePassword(UserPanelViewModel.ChangePasswordViewModel change)
        {
            string currentUserName = User.Identity.Name;
            if (!ModelState.IsValid)
                return View(change);
            if (!_userService.CompareOldPassword(change.OldPassword, currentUserName))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور قبلی وارد شده مغایرت دارد ");
                return View(change);
            }

            _userService.ChangeUserPassword(currentUserName, change.Password);
            ViewBag.IsSuccess = true;
            return View();
        }
    }
}