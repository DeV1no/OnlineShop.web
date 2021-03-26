using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.DTOs;
using OnlineShop.web.Security;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Users
{
    [PermissionChecker(4)]

    public class EditUser : PageModel
    {
        private IUserService _userService;
        private IPermisionService _permisionService;

        public EditUser(IUserService userService, IPermisionService permisionService)
        {
            _userService = userService;
            _permisionService = permisionService;
        }
        [BindProperty]
        public UsersViewModel.EditUserViewModel EditUserViewModel { get; set; }
        public void OnGet(int id)
        {
            EditUserViewModel = _userService.GetUserForShowInEditMode(id);
            ViewData["Role"] = _permisionService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userService.EditUserFromAdmin(EditUserViewModel);

            //Edit Roles
            _permisionService.EditRolesUser(EditUserViewModel.UserId,SelectedRoles);
            return RedirectToPage("Index");
        }
    }
}