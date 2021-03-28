using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Courses
{
    public class CreateEpisode : PageModel
    {
        private ICourseSerervice _courseSerervice;

        public CreateEpisode(ICourseSerervice courseSerervice)
        {
            _courseSerervice = courseSerervice;
        }

        [BindProperty] public CourseEpisode CourseEpisode { get; set; }

        public void OnGet(int id)
        {
            ViewData["CourseId"] = id;
            CourseEpisode = new CourseEpisode();
            CourseEpisode.CourseId = id;
        }

        public IActionResult OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid || fileEpisode == null)
                return Page();
            if (_courseSerervice.ChecExistFile(fileEpisode.FileName))
            {
                ViewData["IsExistfile"] = true;
                return Page();
            }

            _courseSerervice.AddEpisode(CourseEpisode, fileEpisode);
            return Redirect("/Admin/Courses/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}