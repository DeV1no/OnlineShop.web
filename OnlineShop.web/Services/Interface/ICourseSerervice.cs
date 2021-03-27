using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.web.Entities.Course;

namespace OnlineShop.web.Services.Interface
{
    public interface ICourseSerervice
    {
        List<CourseGroup> GetAllGroup();
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int groupId);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatus();
    }
}