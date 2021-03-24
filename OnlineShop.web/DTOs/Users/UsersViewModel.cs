using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.DTOs
{
    public class UsersViewModel
    {
        public class UsersForAdminViewModel
        {
            public List<User> Users { get; set; }
            public int CurrentPage { get; set; }
            public int PageCount { get; set; }
        }

        public class CreateUserViewModel
        {
            [Display(Name = "نام کاربری")]
            [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
            public string UserName { get; set; }

            [Display(Name = " پست الکترونیکی")]
            [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
            [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
            public string Email { get; set; }

            [Display(Name = " رمز عبور")]
            [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
            public string Password { get; set; }

            public IFormFile UserAvatar { get; set; }
            //  public List<int> SelectedRoles { get; set; }
        }

        public class EditUserViewModel : CreateUserViewModel
        {
            public int UserId { get; set; }
            public List<int> UserRoles { get; set; }
            public string AvatarName { get; set; }
        }
    }
}