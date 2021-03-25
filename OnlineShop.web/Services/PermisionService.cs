using System.Collections.Generic;
using System.Linq;
using OnlineShop.DataLayer.Context;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Entities.Permissions;
using OnlineShop.web.Migrations;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Services
{
    public class PermisionService : IPermisionService
    {
        private OnlineShopeContext _context;

        public PermisionService(OnlineShopeContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public Role GetRoleById(int RoleId)
        {
            return _context.Roles.Find(RoleId);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            _context.SaveChanges();
        }

        public void EditRolesUser(int userId, List<int> roleId)
        {
            // Delete all roles
            _context.UserRoles.Where(r => r.UserId == userId).ToList()
                .ForEach(r => _context.UserRoles.Remove(r));

            //Add new roles
            AddRolesToUser(roleId, userId);
        }

        public List<Permission> GetAllPermision()
        {
            return _context.Permision.ToList();
        }

        public void AddPermisonsToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                _context.RolePermisson.Add(new RolePermisson()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
                _context.SaveChanges();
            }
        }

        public List<int> permissionsRole(int roleId)
        {
            return _context.RolePermisson.Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToList();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _context.RolePermisson.Where(p=>p.RoleId==roleId)
                .ToList().ForEach(p=> _context.RolePermisson.Remove(p));

            AddPermisonsToRole(roleId,permissions);
        }
    }
}