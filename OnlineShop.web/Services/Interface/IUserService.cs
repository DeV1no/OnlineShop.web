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
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string userName);
        int GetUserIdByUserName(string userName);
        void UpdateUser(User user);

        bool ActiveAccount(string activeCode);

        // user panel
        UserPanelViewModel.InformationUserViewModel GetUserInformation(string username);
        UserPanelViewModel.SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
        UserPanelViewModel.EditProfileViewModel GetDataForEditProfileUser(string username);
        void EditProfile(string username, UserPanelViewModel.EditProfileViewModel profile);
        bool CompareOldPassword(string oldPassword, string username);
        void ChangeUserPassword(string username, string newPassword);

        // Wallet Service
        int BalanceUserWallet(string username);
        List<WalletViewModel.WalletReportViewModel> GetWalletUser(string username);
        void ChargeWallet(string username, int amount, string description, bool isPay = false);
        void AddWallet(Wallet wallet);
    }
}