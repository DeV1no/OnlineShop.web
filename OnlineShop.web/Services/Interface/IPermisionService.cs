using System.Collections.Generic;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Services.Interface
{
    public interface IPermisionService
    {
        //Roles
        List<Role> GetRoles();
        void AddRolesToUser(List<int> roleIds, int userId);
    }
}