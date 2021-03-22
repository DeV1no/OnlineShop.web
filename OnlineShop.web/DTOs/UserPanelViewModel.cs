using System;

namespace OnlineShop.web.DTOs
{
    public class UserPanelViewModel
    {
        public class InformationUserViewModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public DateTime RegisterDate { get; set; }
            public int Wallet { get; set; }
        }

        public class SideBarUserPanelViewModel
        {
            public string UserName { get; set; }
            public DateTime RegisterDate { get; set; }
            public string ImageName { get; set; }
        }
    }
}