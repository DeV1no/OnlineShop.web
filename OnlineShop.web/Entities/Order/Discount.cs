using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Entities.Order
{
    public class Discount
    {
        [Key] public int DiscountId { get; set; }

        [Required]
        [Display(Name = "کد تخفیف")]
        public string DiscountCode { get; set; }


        [Required]
        [Display(Name = "درصد تخفیف")]
        public int Percent { get; set; }

        [Display(Name = "تعداد استفاده")] public int? UsableCount { get; set; }
        [Display(Name = "تاریخ شروع")] public DateTime? StartDate { get; set; }
        [Display(Name = "تاریخ پایان")] public DateTime? EndDate { get; set; }

        // RelationShip
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
    }
}