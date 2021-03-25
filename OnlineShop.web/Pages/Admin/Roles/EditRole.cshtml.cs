using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Roles
{
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
        }
        
      

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            //update

            _permisionService.UpdateRole(Role);

            // todo Add permision
            return RedirectToPage("Index");
        }
    }
}