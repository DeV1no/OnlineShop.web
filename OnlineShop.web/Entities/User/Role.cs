
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {
            
        }
        [Key] public int RoleId { get; set; }

        [Display(Name = "")]
        [Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public string RoleTitle { get; set; }

        #region Relationship

        public virtual List<UserRole> userRoles { get; set; }

        #endregion
    }
}