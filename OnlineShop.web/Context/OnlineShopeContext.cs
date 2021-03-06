using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Entities.Order;
using OnlineShop.web.Entities.Permissions;
using OnlineShop.web.Entities.Wallet;
using OnlineShop.web.Views.Course;

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
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

        #endregion

        #region Wallet

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallet { get; set; }

        #endregion

        #region Permisions

        public DbSet<Permission> Permision { get; set; }
        public DbSet<RolePermisson> RolePermisson { get; set; }

        #endregion

        #region Course

        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);
            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(c => !c.IsDelete);


            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }


        #region seadData

        //permissions
        private void SeedData(ModelBuilder modelBuilder)
        {
            var adminPanel = new Permission() {PermissionId = 1, permissionTitle = "پنل مدیریت",};
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
                    adminPanel, userManagment, addUser, editUser, deleteUser, roleManagment, addRole, editRole,
                    deleteRole
                });
        }

        #endregion
    }
}