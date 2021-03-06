using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.web.DTOs.Order;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrdersController : Controller
    {
        private IOrderService _orderedService;

        public MyOrdersController(IOrderService orderedService)
        {
            _orderedService = orderedService;
        }

        //  GET
        public IActionResult Index()
        {
            return View(_orderedService.GetUserOrders(User.Identity.Name));
        }

        public IActionResult ShowOrder(int id, bool finaly = false,string type="")
        {
            var order = _orderedService.GetOrderGorUserPanle(User.Identity.Name, id);
            if (order == null)
                return NotFound();
            ViewBag.typeDiscount = type;
            ViewBag.finaly = finaly;
            return View(order);
        }

        public IActionResult FinallyOrder(int id)
        {
            if (_orderedService.FinallyOrder(User.Identity.Name, id))
            {
                return Redirect("/UserPanel/MyOrders/showOrder/" + id + "?finaly=true");
            }

            return BadRequest();
        }

        public IActionResult UseDiscount(int orderId, string code)
        {
            DiscountUseType type = _orderedService.UseDiscount(orderId, code);
            return Redirect("/Userpanel/myorders/showorder/" + orderId + "?type=" + type.ToString());
        }
    }
}