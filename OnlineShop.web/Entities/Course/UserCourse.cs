using System;
using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Entities.Course
{
    public class UserCourse
    {
        [Key] public int UC_id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        //RelationShip

        public Course Course { get; set; }
        public User User { get; set; }
    }
}