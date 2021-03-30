using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseSerervice _courseSerervice;

        public CourseController(ICourseSerervice courseSerervice)
        {
            _courseSerervice = courseSerervice;
        }

        public IActionResult Index(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            ViewBag.getType = getType;
            ViewBag.pageId = pageId;
            ViewBag.Groups = _courseSerervice.GetAllGroup();
            ViewBag.selectedGroups = selectedGroups;
            return View(_courseSerervice.GetCourse(pageId, filter, getType, orderByType, startPrice, endPrice,
                selectedGroups, 9));
        }
    }
}