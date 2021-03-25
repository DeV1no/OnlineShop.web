using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Entities.Permissions
{
    public class RolePermisson
    {
        [Key]
        public int RP_id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}