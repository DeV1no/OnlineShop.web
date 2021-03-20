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
    }
}