using System.ComponentModel.DataAnnotations;

namespace OnlineShop.web.Entities.Order
{
    public class OrderDetail
    {
        [Key] public int DetailId { get; set; }
        [Required] public int OrderId { get; set; }
        [Required] public int CourseId { get; set; }
        [Required] public int Count { get; set; }
        [Required] public int Price { get; set; }

        //RelationShip
        
        public Order Order { get; set; }
        public Course.Course Course { get; set; }
    }
}