using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OnlineShop.web.Models;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private ICourseSerervice _courseSerervice;

        public HomeController(ILogger<HomeController> logger, IUserService userService,
            ICourseSerervice courseSerervice)
        {
            _logger = logger;
            _userService = userService;
            _courseSerervice = courseSerervice;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != ""
            )
            {
                string authority = HttpContext.Request.Query["Authority"];
                var wallet = _userService.GetWaletByWalletId(id);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }
            }

            return View();
        }

        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem() {Text = "انتخاب کنید", Value = ""}
            };
            list.AddRange(_courseSerervice.GetSubGroupForManageCourse(id));
            // var subgroups = _courseSerervice.GetSubGroupForManageCourse(id);
            return Json(new SelectList(list, "Value", "Text"));
        }
    }
}