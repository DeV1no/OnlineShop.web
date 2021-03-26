using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Entities.Permissions;
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

        #region Permisions

        public DbSet<Permission> Permision { get; set; }
        public DbSet<RolePermisson> RolePermisson { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);
            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
           
        }


        #region seadData

        //permissions
        private void SeedData(ModelBuilder modelBuilder)
        {
            var adminPanel = new Permission() {PermissionId = 1, permissionTitle = "پنل مدیریت", };
            var userManagment = new Permission() {PermissionId = 2, permissionTitle = "مدیریت کاربران", ParentId = 1};
            var addUser = new Permission() {PermissionId = 3, permissionTitle = "افزودن کاربر", ParentId = 2};
            var editUser = new Permission() {PermissionId = 4, permissionTitle = "ویرایش کاربران", ParentId = 2};
            var deleteUser = new Permission() {PermissionId = 5, permissionTitle = "حدف کاربران", ParentId = 2};
            var roleManagment = new Permission() {PermissionId = 6, permissionTitle = "مدیریت  نقش ها", ParentId = 1};
            var addRole = new Permission() {PermissionId = 7, permissionTitle = "افزودن نقش", ParentId = 6};
            var editRole = new Permission() {PermissionId = 8, permissionTitle = "ویرایش نقش", ParentId = 6};
            var deleteRole = new Permission() {PermissionId = 9, permissionTitle = "حذف نقش", ParentId = 6};
            modelBuilder.Entity<Permission>()
                .HasData(new List<Permission>
                {
                   adminPanel,userManagment,addUser,editUser,deleteUser,roleManagment,addRole,editRole,deleteRole 
                });
        }

        #endregion
    }
}