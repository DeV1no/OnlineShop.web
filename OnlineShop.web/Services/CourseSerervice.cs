using System.Collections.Generic;
using System.Linq;
using OnlineShop.DataLayer.Context;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Services
{
    public class CourseSerervice:ICourseSerervice
    {
        private OnlineShopeContext _context;

        public CourseSerervice(OnlineShopeContext context)
        {
            _context = context;
        }

        public List<CourseGroup> GetAllGroup()
        {
          return _context.CourseGroups.ToList();
        }
    }
}