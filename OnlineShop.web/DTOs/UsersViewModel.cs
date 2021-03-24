using System.Collections.Generic;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.DTOs
{
    public class UsersViewModel
    {
        public class UsersForAdminViewModel
        {
            public List<User> Users { get; set; }
            public int CurrentPage { get; set; }
            public int PageCount { get; set; }
        }
    }
}