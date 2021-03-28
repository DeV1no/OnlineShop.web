using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Courses
{
    public class EditCourse : PageModel
    {
        private ICourseSerervice _courseSerervice;

        public EditCourse(ICourseSerervice courseSerervice)
        {
            _courseSerervice = courseSerervice;
        }

        [BindProperty] public Course Course { get; set; }

        public void OnGet(int id)
        {
            Course = _courseSerervice.GetCourseById(id);
            var groups = _courseSerervice.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Course.GroupId);

            var subgroups = _courseSerervice.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subgroups, "Value", "Text", Course.SubGroup ?? 0);
            var teachers = _courseSerervice.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", Course.TeacherId);
            var levels = _courseSerervice.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text", Course.LevelId);

            var status = _courseSerervice.GetStatus();
            ViewData["Statues"] = new SelectList(status, "Value", "Text", Course.StatusId);
        }

        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
                return Page();
            _courseSerervice.UpdateCourse(Course, imgCourseUp, demoUp);
            return RedirectToPage("Index");
        }
    }
}