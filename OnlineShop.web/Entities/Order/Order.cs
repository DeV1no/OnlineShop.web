using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Entities.Order
{
    public class Order
    {
        [Key] public int OrderId { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public int OrderSum { get; set; }
        public bool IsFinaly { get; set; }
        [Required] public DateTime CreateDate { get; set; }

        //RelationShip

        public virtual User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}