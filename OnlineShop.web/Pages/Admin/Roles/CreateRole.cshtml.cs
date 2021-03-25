using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Roles
{
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
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            //add role
            Role.IsDelete = false;
            int roleId = _permisionService.AddRole(Role);

            // todo Add permision
            return RedirectToPage("Index");
        }
    }
}