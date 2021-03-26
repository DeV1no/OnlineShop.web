using System.Collections.Generic;
using OnlineShop.web.Entities.Course;

namespace OnlineShop.web.Services.Interface
{
    public interface ICourseSerervice
    {
        List<CourseGroup> GetAllGroup();
    }
}