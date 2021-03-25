using System.Collections.Generic;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.DTOs;
using OnlineShop.web.Entities.Wallet;

namespace OnlineShop.web.Services.Interface
{
    public interface IUserService
    {
        bool isExistUserName(string userName);
        bool isExistEmail(string email);
        int AddUser(User user);
        User LoginUser(AccountViewModel.LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserById(int UserId);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string userName);
        int GetUserIdByUserName(string userName);
        void UpdateUser(User user);

        bool ActiveAccount(string activeCode);

        void DeleteUser(int userId);

        void RecoverUser(int userId);

        // user panel
        UserPanelViewModel.InformationUserViewModel GetUserInformation(string username);
        UserPanelViewModel.InformationUserViewModel GetUserInformation(int userId);
        UserPanelViewModel.SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
        UserPanelViewModel.EditProfileViewModel GetDataForEditProfileUser(string username);
        void EditProfile(string username, UserPanelViewModel.EditProfileViewModel profile);
        bool CompareOldPassword(string oldPassword, string username);
        void ChangeUserPassword(string username, string newPassword);

        // Wallet Service
        int BalanceUserWallet(string username);
        List<WalletViewModel.WalletReportViewModel> GetWalletUser(string username);
        int ChargeWallet(string username, int amount, string description, bool isPay = false);
        int AddWallet(Wallet wallet);
        Wallet GetWaletByWalletId(int walletId);
        void UpdateWallet(Wallet wallet);

        // admin panel
        UsersViewModel.UsersForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "",
            string filterUserName = "");

        UsersViewModel.UsersForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "",
            string filterUserName = "");

        int AddUserFromAdmin(UsersViewModel.CreateUserViewModel user);
        UsersViewModel.EditUserViewModel GetUserForShowInEditMode(int userId);
        void EditUserFromAdmin(UsersViewModel.EditUserViewModel editUser);
    }
}