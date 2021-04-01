using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseSerervice _courseSerervice;
        private IOrderService _orderService;

        public CourseController(ICourseSerervice courseSerervice, IOrderService orderService)
        {
            _courseSerervice = courseSerervice;
            _orderService = orderService;
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

        [Route("ShowCourse/{id}")]
        public IActionResult ShowCourse(int id)
        {
            var course = _courseSerervice.GetCourseForShow(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [Authorize]
        public ActionResult BuyCourse(int id)
        {
            int orderId = _orderService.AddOrder(User.Identity.Name, id);
            return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId);
        }
    }
}