using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.DTOs;

namespace OnlineShop.web.Services.Interface
{
    public interface IUserService
    {
        bool isExistUserName(string userName);
        bool isExistEmail(string email);
        int AddUser(User user);
        User LoginUser(AccountViewModel.LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string userName);
        void UpdateUser(User user);

        bool ActiveAccount(string activeCode);

        // user panel
        UserPanelViewModel.InformationUserViewModel GetUserInformation(string username);
        UserPanelViewModel.SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
    }
}