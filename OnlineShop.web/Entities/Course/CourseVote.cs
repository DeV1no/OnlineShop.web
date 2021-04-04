using System;
using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.web.Views.Course
{
    public class CourseVote
    {
        [Key] public int VoteId { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public int CourseId { get; set; }
        [Required] public bool Vote { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.Now;

        // RelationShip
        public User User { get; set; }
        public Entities.Course.Course Course { get; set; }
    }
}