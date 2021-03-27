using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.DTOs.Course;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Courses
{
    public class Index : PageModel
    {
        private ICourseSerervice _courseService;

        public Index(ICourseSerervice courseService)
        {
            _courseService = courseService;
        }

        public List<ShowCourseForAdminViewModel> ListCourse { get; set; }

        public void OnGet()
        {
            ListCourse = _courseService.GetCoursesForAdmin();
        }
    }
}