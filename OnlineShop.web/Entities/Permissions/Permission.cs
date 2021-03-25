using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.web.Entities.Permissions
{
    public class Permission
    {
        [Key] public int PermissionId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string permissionTitle { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")] public List<Permission> permissions { get; set; }
        public List<RolePermisson> RolePermissons { get; set; }
    }
}