using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseSerervice _courseSerervice;
        private IOrderService _orderService;
        private IUserService _userService;

        public CourseController(ICourseSerervice courseSerervice, IOrderService orderService, IUserService userService)
        {
            _courseSerervice = courseSerervice;
            _orderService = orderService;
            _userService = userService;
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

        [Route("DownloadFile/{episodeId}")]
        public IActionResult DownloadFile(int episodeId)
        {
            var episode = _courseSerervice.GetEpisodeById(episodeId);
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles",
                episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;
            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filepath);
                return File(file, "application/force-download", fileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (_orderService.isUserInCourse(User.Identity.Name, episode.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filepath);
                    return File(file, "application/force-download", fileName);
                }
            }

            return Forbid();
        }


        [HttpPost]
        public IActionResult CreateComment(CourseComment comment)
        {
            comment.IsDelete = false;
            comment.CreatedDate = DateTime.Now;
            comment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
            _courseSerervice.AddComments(comment);
           return View("ShowComment",_courseSerervice.GetCoruseComment(comment.CourseId));
            
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return View(_courseSerervice.GetCoruseComment(id, pageId));
        }
    }
}