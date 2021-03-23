using System;
using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Entities.Wallet
{
    public class Wallet
    {
        public Wallet()
        {
        }

        [Key] public int WalletId { get; set; }

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public int TypeId { get; set; }

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public int UserId { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "شرح")] public string Description { get; set; }
        [Display(Name = "تایید شده")] public bool IsPay { get; set; }
        [Display(Name = "تاریخ و ساعت")] public DateTime CreatedDate { get; set; }


        // Reletions
        public virtual User User { get; set; }
        public virtual WalletType Type { get; set; }
    }
}