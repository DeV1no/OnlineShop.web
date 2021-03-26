using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.DTOs;
using OnlineShop.web.Security;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Users
{
    [PermissionChecker(5)]

    public class DeleteUser : PageModel
    {
        private IUserService _userService;

        public DeleteUser(IUserService userService)
        {
            _userService = userService;
        }

        public UserPanelViewModel.InformationUserViewModel InformationUserViewModel { get; set; }

        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            InformationUserViewModel = _userService.GetUserInformation(id);
        }

        public IActionResult OnPost(int userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToPage("Index");
        }
    }
}