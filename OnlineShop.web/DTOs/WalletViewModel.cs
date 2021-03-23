using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.web.DTOs
{
    public class WalletViewModel
    {
        public class WalletChargeViewModle
        {
            [Display(Name = "مبلغ")]
            [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
            public int Amount { get; set; }
        }

        public class WalletReportViewModel
        {
            public int Amount { get; set; }
            public int Type { get; set; }
            public string Description { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
}