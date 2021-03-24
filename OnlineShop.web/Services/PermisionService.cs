using System.Collections.Generic;
using System.Linq;
using OnlineShop.DataLayer.Context;
using OnlineShop.DataLayer.Entities.User;
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

        public  List<Role> GetRoles()
        {
            return _context.Roles.ToList();
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
            _context.UserRoles.Where(r=>r.UserId==userId).ToList()
                .ForEach(r=>_context.UserRoles.Remove(r));
            
            //Add new roles
            AddRolesToUser(roleId, userId);
        }
    }
}