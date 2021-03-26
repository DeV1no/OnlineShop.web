using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Security;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Roles
{
    [PermissionChecker(9)]

    public class DeleteRole : PageModel
    {
        private IPermisionService _permisionService;

        public DeleteRole(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        [BindProperty] public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role = _permisionService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            _permisionService.DeleteRole(Role);
            return RedirectToPage("Index");
        }
    }
}