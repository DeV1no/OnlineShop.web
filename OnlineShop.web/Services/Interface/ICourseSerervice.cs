using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.web.DTOs.Course;
using OnlineShop.web.Entities.Course;

namespace OnlineShop.web.Services.Interface
{
    public interface ICourseSerervice
    {
        //Group
        List<CourseGroup> GetAllGroup();
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int groupId);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatus();

        // Course
        int AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);
        List<ShowCourseForAdminViewModel> GetCoursesForAdmin();
        Course GetCourseById(int courseId);

        void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo);

        //Episode
        List<CourseEpisode> GetListEpisode(int courseId);
        int AddEpisode(CourseEpisode episode, IFormFile episodeFile);
        bool ChecExistFile(string fileName);
        CourseEpisode GetEpisodeById(int episodeId);
        void EditEpisode(CourseEpisode episode, IFormFile episodeFile);
    }
}