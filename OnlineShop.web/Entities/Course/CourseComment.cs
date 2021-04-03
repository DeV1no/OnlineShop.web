using System;
using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Entities.Course
{
    public class CourseComment
    {
        [Key] public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        [MaxLength(700)] public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminRead { get; set; }

        //RelationShip
        public Course Course { get; set; }
        public User User { get; set; }
    }
}