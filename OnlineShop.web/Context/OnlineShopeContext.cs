using Microsoft.EntityFrameworkCore;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Entities.Wallet;

namespace OnlineShop.DataLayer.Context
{
    public class OnlineShopeContext : DbContext
    {
        public OnlineShopeContext(DbContextOptions<OnlineShopeContext> options) : base(options)
        {
        }

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Wallet

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallet { get; set; }

        #endregion
    }
}