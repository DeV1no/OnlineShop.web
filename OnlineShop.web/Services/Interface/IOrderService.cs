using System.Collections.Generic;
using OnlineShop.web.DTOs.Order;
using OnlineShop.web.Entities.Order;

namespace OnlineShop.web.Services.Interface
{
    public interface IOrderService
    {
        int AddOrder(string userName, int courseId);
        void UpdatePriceOrder(int orderId);
        Order GetOrderGorUserPanle(string userName, int orderId);
        Order GetOrderById(int orderId);
        bool FinallyOrder(string userName, int orderId);
        List<Order> GetUserOrders(string userName);

        void UpdateOrder(Order order);

        // Discount
        DiscountUseType UseDiscount(int orderId, string code);
    }
}