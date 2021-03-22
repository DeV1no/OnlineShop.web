using System.IO;
using System.Linq;
using OnlineShop.DataLayer.Context;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Convertor;
using OnlineShop.web.DTOs;
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
            information.Wallet = 0;
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
    }
}