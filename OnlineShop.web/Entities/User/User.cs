using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {
        }


        [Key] public int UserId { get; set; }

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

        [Display(Name = "کد فعال سازی")] public string ActiveCode { get; set; }

        [Display(Name = "وضعیت")] public bool IsActive { get; set; }

        [Display(Name = "نمایه")] public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام")] public string RegisterDate { get; set; }

        #region Relationship

        public virtual List<UserRole> UserRoles { get; set; }

        #endregion
    }
}