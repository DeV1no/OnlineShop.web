using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.DTOs;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Roles
{
    public class Index : PageModel
    {
        private IPermisionService _permisionService;

        public Index(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        public List<DataLayer.Entities.User.Role> RolesList { get; set; }

        public void OnGet()
        {
            RolesList = _permisionService.GetRoles();
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}