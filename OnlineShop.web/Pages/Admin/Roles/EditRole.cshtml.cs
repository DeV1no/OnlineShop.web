using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Security;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Roles
{
    [PermissionChecker(8)]

    public class EditRole : PageModel
    {
        private IPermisionService _permisionService;

        public EditRole(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        [BindProperty] public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role = _permisionService.GetRoleById(id);
            ViewData["Permissions"] = _permisionService.GetAllPermision();
            ViewData["SelectedPermissions"] = _permisionService.permissionsRole(id);
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();


            _permisionService.UpdateRole(Role);

            _permisionService.UpdatePermissionsRole(Role.RoleId, SelectedPermission);

            return RedirectToPage("Index");
        }
    }
}

