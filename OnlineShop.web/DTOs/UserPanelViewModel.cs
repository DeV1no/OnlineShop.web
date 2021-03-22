using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.web.DTOs
{
    public class UserPanelViewModel
    {
        public class InformationUserViewModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public DateTime RegisterDate { get; set; }
            public int Wallet { get; set; }
        }

        public class SideBarUserPanelViewModel
        {
            public string UserName { get; set; }
            public DateTime RegisterDate { get; set; }
            public string ImageName { get; set; }
        }

        public class EditProfileViewModel
        {
            [Display(Name = "نام کاربری")]
            [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
            public string UserName { get; set; }

            [Display(Name = " پست الکترونیکی")]
            [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
            [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
            public string Email { get; set; }

            public IFormFile UserAvatar { get; set; }
            public string AvatarName { get; set; }
        }
    }
}