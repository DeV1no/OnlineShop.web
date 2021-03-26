using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.Security;

namespace OnlineShop.web.Pages.Admin
{   
    [PermissionChecker(1)]

    public class Index : PageModel
    {
        public void OnGet()
        {
            
        }
        
    }
}