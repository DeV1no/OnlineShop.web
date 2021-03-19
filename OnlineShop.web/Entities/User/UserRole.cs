using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.User
{
    
    public class UserRole
    {
        public UserRole()
        {
            
        }
        [Key] public int URId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Reletions

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        #endregion
    }
}