using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OnlineShop.DataLayer.Context;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Convertor;
using OnlineShop.web.DTOs;
using OnlineShop.web.Entities.Wallet;
using OnlineShop.web.Generrator;
using OnlineShop.web.Security;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Services
{
    public class UserService : IUserService
    {
        private OnlineShopeContext _context;

        public UserService(OnlineShopeContext context)
        {
            _context = context;
        }


        public bool isExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public bool isExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(AccountViewModel.LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixText.FixEmail(login.Email);
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _context.SaveChanges();
            return true;
        }

        public UserPanelViewModel.InformationUserViewModel GetUserInformation(string username)
        {
            var user = GetUserByUserName(username);
            UserPanelViewModel.InformationUserViewModel information =
                new UserPanelViewModel.InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = BalanceUserWallet(username);
            return information;
        }

        public UserPanelViewModel.SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            return _context.Users.Where(u => u.UserName == username).Select(u =>
                new UserPanelViewModel.SideBarUserPanelViewModel
                {
                    UserName = u.UserName,
                    ImageName = u.UserAvatar,
                    RegisterDate = u.RegisterDate
                }).Single();
        }

        public UserPanelViewModel.EditProfileViewModel GetDataForEditProfileUser(string username)
        {
            return _context.Users.Where(u => u.UserName == username).Select(u =>
                new UserPanelViewModel.EditProfileViewModel()
                {
                    AvatarName = u.UserAvatar,
                    Email = u.Email,
                    UserName = u.UserName,
                }).Single();
        }

        public void EditProfile(string username, UserPanelViewModel.EditProfileViewModel profile)
        {
            if (profile.UserAvatar != null)
            {
                string imagePath = "";

                if (profile.AvatarName != "Defult.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                    if (File.Exists(imagePath))
                        File.Delete(imagePath);
                }

                profile.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }
            }

            var user = GetUserByUserName(username);
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatarName;
            UpdateUser(user);
        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users.Any(u => u.UserName == username && u.Password == hashOldPassword);
        }

        public void ChangeUserPassword(string username, string newPassword)
        {
            var user = GetUserByUserName(username);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }

        public int BalanceUserWallet(string username)
        {
            int userId = GetUserIdByUserName(username);
            var enter = _context.Wallet
                .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay)
                .Select(w => w.Amount).ToList();
            var exit = _context.Wallet
                .Where(w => w.UserId == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToList();
            return (enter.Sum() - exit.Sum());
        }

        public List<WalletViewModel.WalletReportViewModel> GetWalletUser(string username)
        {
            int userId = GetUserIdByUserName(username);
            return _context.Wallet.Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new WalletViewModel.WalletReportViewModel()
                {
                    Amount = w.Amount,
                    DateTime = w.CreatedDate,
                    Description = w.Description,
                    Type = w.TypeId
                })
                .ToList();
        }

        public int ChargeWallet(string username, int amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet
            {
                Amount = amount,
                CreatedDate = DateTime.Now,
                Description = "شارژ کیف پول",
                IsPay = isPay,
                TypeId = 1,
                UserId = GetUserIdByUserName(username)
            };
            return AddWallet(wallet);
        }

        public int AddWallet(Wallet wallet)
        {
            _context.Wallet.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public Wallet GetWaletByWalletId(int walletId)
        {
            return _context.Wallet.Find(walletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallet.Update(wallet);
            _context.SaveChanges();
        }
    }
}