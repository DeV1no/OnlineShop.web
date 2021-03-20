using System.ComponentModel.DataAnnotations;

namespace OnlineShop.web.DTOs
{
    public class AccountViewModel
    {
        public class RegisterViewModel
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

            [Display(Name = " تکرار رمز عبور")]
            [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
            [Compare("Password", ErrorMessage = "رمز های عبور مغایرت دارند")]
            public string RePassword { get; set; }
        }
    }
}