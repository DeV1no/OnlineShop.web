using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.DTOs;
using OnlineShop.web.Security;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Users
{
    [PermissionChecker(3)]

    public class CreateUser : PageModel
    {
        private IUserService _userService;
        private IPermisionService _permisionService;

        public CreateUser(IUserService userService, IPermisionService permisionService)
        {
            _userService = userService;
            _permisionService = permisionService;
        }

        [BindProperty] public UsersViewModel.CreateUserViewModel CreateUserViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Roles"] = _permisionService.GetRoles();
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            ViewData["Roles"] = _permisionService.GetRoles();

            if (!ModelState.IsValid)
                return Page();
            int userId = _userService.AddUserFromAdmin(CreateUserViewModel);
            // Add Roles
            _permisionService.AddRolesToUser(selectedRoles, userId);
            return Redirect("/Admin/Users");
        }
    }
}