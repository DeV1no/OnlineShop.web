using System.Collections.Generic;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.web.Entities.Permissions;
using OnlineShop.web.Migrations;

namespace OnlineShop.web.Services.Interface
{
    public interface IPermisionService
    {
        //Roles
        List<Role> GetRoles();
        int AddRole(Role role);
        Role GetRoleById(int RoleId);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void AddRolesToUser(List<int> roleIds, int userId);

        void EditRolesUser(int userId, List<int> roleId);

        //Permisions 
        List<Permission> GetAllPermision();
        void AddPermisonsToRole(int roleId, List<int> permission);
        List<int> permissionsRole(int roleId);
        void UpdatePermissionsRole(int roleId, List<int> permissions);
    }
}