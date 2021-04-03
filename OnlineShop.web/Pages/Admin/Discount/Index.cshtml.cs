using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Pages.Admin.Discount
{
    public class Index : PageModel
    {
        private IOrderService _orderService;

        public Index(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty] public List<Entities.Order.Discount> Discounts { get; set; }

        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscount();
        }
    }
}