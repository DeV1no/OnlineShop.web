using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Security;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Roles
{
    [PermissionChecker(7)]

    public class CreateRole : PageModel
    {
        private IPermisionService _permisionService;

        public CreateRole(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        [BindProperty] public Role Role { get; set; }

        public void OnGet()
        {
            ViewData["Permissions"] = _permisionService.GetAllPermision();
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();
            //add role
            Role.IsDelete = false;
            int roleId = _permisionService.AddRole(Role);

            //  Add permision
            _permisionService.AddPermisonsToRole(roleId, SelectedPermission);
            return RedirectToPage("Index");
        }
    }
}