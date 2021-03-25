using System.Collections.Generic;
using OnlineShop.DataLayer.Entities.User;

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
    }
}