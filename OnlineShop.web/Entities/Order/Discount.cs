using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.web.Entities.Order
{
    public class Discount
    {
        [Key] public int DiscountId { get; set; }
        [Required] public string DiscountCode { get; set; }
        [Required] public int Percent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}