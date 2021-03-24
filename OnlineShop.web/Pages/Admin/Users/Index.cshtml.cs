using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.DTOs;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Users
{
    public class Index : PageModel
    {
        private IUserService _userService;

        public Index(IUserService userService)
        {
            _userService = userService;
        }

        public UsersViewModel.UsersForAdminViewModel UserForAdminViewModel { get; set; }

        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = _userService.GetUsers(pageId, filterEmail,filterUserName);
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}