using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Services.Interface
{
    public interface IUserService
    {
        bool isExistUserName(string userName);
        bool isExistEmail(string email);
        int AddUser(User user);
    }
}