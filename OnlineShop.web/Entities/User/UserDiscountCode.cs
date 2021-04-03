using System.ComponentModel.DataAnnotations;
using OnlineShop.web.Entities.Order;

namespace OnlineShop.DataLayer.Entities.User
{
    public class UserDiscountCode
    {
        [Key] public int UD_Id { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }

        //RelationShip
        public User User { get; set; }
        public Discount Discount { get; set; }
    }
}