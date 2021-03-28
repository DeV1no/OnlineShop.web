using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Courses
{
    public class EditEpisode : PageModel
    {
        private ICourseSerervice _courseSerervice;

        public EditEpisode(ICourseSerervice courseSerervice)
        {
            _courseSerervice = courseSerervice;
        }

        [BindProperty] public CourseEpisode CourseEpisode { get; set; }

        public void OnGet(int id)
        {
            CourseEpisode = _courseSerervice.GetEpisodeById(id);
            ViewData["CourseId"] = CourseEpisode.CourseId;
        }

        public IActionResult OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid)
                return Page();
            if (fileEpisode != null)
            {
                if (_courseSerervice.ChecExistFile(fileEpisode.FileName))
                {
                    ViewData["IsExistfile"] = true;
                    return Page();
                }
            }

            _courseSerervice.EditEpisode(CourseEpisode, fileEpisode);
            return Redirect("/Admin/Courses/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}