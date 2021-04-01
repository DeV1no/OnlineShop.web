using OnlineShop.web.Entities.Order;

namespace OnlineShop.web.Services.Interface
{
    public interface IOrderService
    {
        int AddOrder(string userName, int courseId);
        void UpdatePriceOrder(int orderId);
        Order GetOrderGorUserPanle(string userName, int orderId);
        bool FinallyOrder(string userName,int orderId);
    }
}