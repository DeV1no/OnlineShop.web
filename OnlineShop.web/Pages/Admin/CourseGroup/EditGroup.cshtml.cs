using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.CourseGroup
{
    public class EditGroup : PageModel
    {
        private ICourseSerervice _courseSerervice;

        public EditGroup(ICourseSerervice courseSerervice)
        {
            _courseSerervice = courseSerervice;
        }

        [BindProperty] public Entities.Course.CourseGroup CourseGroups { get; set; }

        public void OnGet(int id)
        {
            CourseGroups = _courseSerervice.GetGroupById(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            _courseSerervice.UpdateGroup(CourseGroups);
            return RedirectToPage("Index");
        }
    }
}