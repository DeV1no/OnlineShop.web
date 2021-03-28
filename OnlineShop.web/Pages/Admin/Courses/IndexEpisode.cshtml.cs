using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Courses
{
    public class IndexEpisode : PageModel
    {
        private ICourseSerervice _courseSerervice;

        public IndexEpisode(ICourseSerervice courseSerervice)
        {
            _courseSerervice = courseSerervice;
        }

        public List<CourseEpisode> CourseEpisodes { get; set; }
        public void OnGet(int id)
        {
            ViewData["CourseId"] = id;
            CourseEpisodes = _courseSerervice.GetListEpisode(id);
        }
    }
}