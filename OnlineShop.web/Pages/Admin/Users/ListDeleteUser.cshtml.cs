using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.DTOs;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Users
{
    public class ListDeleteUser : PageModel
    {
        private IUserService _userService;

        public ListDeleteUser(IUserService userService)
        {
            _userService = userService;
        }

        public UsersViewModel.UsersForAdminViewModel UserForAdminViewModel { get; set; }

        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = _userService.GetDeleteUsers(pageId, filterEmail, filterUserName);
        }

        public IActionResult OnPost(int id)
        {
            _userService.RecoverUser(id);
            return RedirectToPage("Index");
        }
    }
}