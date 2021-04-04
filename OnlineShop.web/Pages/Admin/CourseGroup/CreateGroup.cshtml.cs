using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.CourseGroup
{
    public class CreateGroup : PageModel
    {
        private ICourseSerervice _courseSerervice;

        public CreateGroup(ICourseSerervice courseSerervice)
        {
            _courseSerervice = courseSerervice;
        }

        [BindProperty] public Entities.Course.CourseGroup CourseGroups { get; set; }

        public void OnGet(int? id)
        {
            CourseGroups = new Entities.Course.CourseGroup()
            {
                ParentId = id
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            _courseSerervice.AddGroup(CourseGroups);
            return RedirectToPage("Index");
        }
    }
}