using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataLayer.Context;
using OnlineShop.web.Convertor;
using OnlineShop.web.DTOs.Course;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Generrator;
using OnlineShop.web.Security;
using OnlineShop.web.Services.Interface;
using OnlineShop.web.Views.Course;

namespace OnlineShop.web.Services
{
    public class CourseSerervice : ICourseSerervice
    {
        private OnlineShopeContext _context;

        public CourseSerervice(OnlineShopeContext context)
        {
            _context = context;
        }

        public List<CourseGroup> GetAllGroup()
        {
            return _context.CourseGroups.Include(c => c.CourseGroups).ToList();
        }

        public List<SelectListItem> GetGroupForManageCourse()
        {
            return _context.CourseGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString(),
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageCourse(int groupId)
        {
            return _context.CourseGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString(),
                }).ToList();
        }

        public List<SelectListItem> GetTeachers()
        {
            return _context.UserRoles.Where(r => r.RoleId == 2).Include(r => r.User)
                .Select(u => new SelectListItem()
                {
                    Value = u.UserId.ToString(),
                    Text = u.User.UserName
                }).ToList();
        }

        public List<SelectListItem> GetStatus()
        {
            return _context.CourseStatuses.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToList();
        }

        public void AddGroup(CourseGroup @group)
        {
            _context.CourseGroups.Add(group);
            _context.SaveChanges();
        }

        public void UpdateGroup(CourseGroup @group)
        {
            _context.CourseGroups.Update(group);
            _context.SaveChanges();
        }

        public CourseGroup GetGroupById(int groupId)
        {
            return _context.CourseGroups.Find(groupId);
        }

        public List<SelectListItem> GetLevels()
        {
            return _context.CourseLevels.Select(l => new SelectListItem()
            {
                Value = l.LevelId.ToString(),
                Text = l.LevelTitle
            }).ToList();
        }

        public int AddCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;
            course.CourseImageName = "no-photo.jpg";
            // Check Image

            if (imgCourse != null && imgCourse.IsImage())
            {
                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image",
                    course.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                // image resize
                ImageConverter imgResizer = new ImageConverter();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb",
                    course.CourseImageName);
                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }


            //Upload Demo 

            if (courseDemo != null)
            {
                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes",
                    course.DemoFileName);

                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            _context.Add(course);
            _context.SaveChanges();

            return course.CourseId;
        }

        public List<ShowCourseForAdminViewModel> GetCoursesForAdmin()
        {
            return _context.Courses.Select(c => new ShowCourseForAdminViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Title = c.CourseTitle,
                EpisodeCount = c.CourseEpisodes.Count
            }).ToList();
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public void UpdateCourse(Course course, IFormFile imgCourse, IFormFile courseDemo)
        {
            course.UpdateDate = DateTime.Now;

            if (imgCourse != null && imgCourse.IsImage())
            {
                if (course.CourseImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image",
                        course.CourseImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb",
                        course.CourseImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }

                course.CourseImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgCourse.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/image",
                    course.CourseImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgCourse.CopyTo(stream);
                }

                // image resize
                ImageConverter imgResizer = new ImageConverter();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/thumb",
                    course.CourseImageName);
                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }


            //Upload Demo 

            if (courseDemo != null)
            {
                if (course.DemoFileName != null)
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes",
                        course.DemoFileName);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                }

                course.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/demoes",
                    course.DemoFileName);

                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }
            }

            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public Tuple<List<ShowCourseListItemViewModel>, int> GetCourse(int pageId = 1, string filter = ""
            , string getType = "all", string orderByType = "date",
            int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
                take = 8;

            IQueryable<Course> result = _context.Courses;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter));
            }

            switch (getType)
            {
                case "all":
                    break;
                case "buy":
                {
                    result = result.Where(c => c.CoursePrice != 0);
                    break;
                }
                case "free":
                {
                    result = result.Where(c => c.CoursePrice == 0);
                    break;
                }
            }

            switch (orderByType)
            {
                case "date":
                {
                    result = result.OrderByDescending(c => c.CreateDate);
                    break;
                }
                case "updatedate":
                {
                    result = result.OrderByDescending(c => c.UpdateDate);
                    break;
                }
            }

            if (startPrice > 0)
            {
                result = result.Where(c => c.CoursePrice > startPrice);
            }

            if (endPrice > 0)
            {
                result = result.Where(c => c.CoursePrice < startPrice);
            }


            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (var groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubGroup == groupId);
                }
            }

            int skip = (pageId - 1) * take;
            int pageCount = result.Include(c => c.CourseEpisodes).Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                TotalTime = new TimeSpan(20)
                //     new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).Count() / take;
            var query = result.Include(c => c.CourseEpisodes).Select(c => new ShowCourseListItemViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                TotalTime = new TimeSpan(20)
                //     new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).Skip(skip).Take(take).ToList();
            return Tuple.Create(query, pageCount);
        }

        public Course GetCourseForShow(int courseId)
        {
            return _context.Courses
                .Include(e => e.CourseEpisodes)
                .Include(c => c.CourseStatus)
                .Include(c => c.CourseLevel)
                .Include(c => c.User)
                .Include(c => c.UserCourse)
                .FirstOrDefault(c => c.CourseId == courseId);
            ;
        }

        public bool IsFree(int coursId)
        {
            return _context.Courses.Where(c => c.CourseId == coursId)
                .Select(c => c.CoursePrice).First()==0;
        }

        public List<CourseEpisode> GetListEpisode(int courseId)
        {
            return _context.CourseEpisodes.Where(e => e.CourseId == courseId).ToList();
        }

        public int AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.EpisodeFileName = episodeFile.FileName;

            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/CourseFiles",
                episode.EpisodeFileName);

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                episodeFile.CopyTo(stream);
            }

            _context.CourseEpisodes.Add(episode);
            _context.SaveChanges();
            return episode.EpisodeId;
        }

        public bool ChecExistFile(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/CourseFiles",
                fileName);
            return File.Exists(path);
        }

        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }

        public void EditEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                string deleteFilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/CourseFiles",
                    episode.EpisodeFileName);
                File.Delete(deleteFilepath);
                episode.EpisodeFileName = episodeFile.FileName;
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/course/CourseFiles",
                    episode.EpisodeFileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    episodeFile.CopyTo(stream);
                }
            }

            _context.CourseEpisodes.Update(episode);
            _context.SaveChanges();
        }

        public void AddComments(CourseComment comment)
        {
            _context.CourseComments.Add(comment);
            _context.SaveChanges();
        }

        public Tuple<List<CourseComment>, int> GetCoruseComment(int courseId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            int pageCount = _context.CourseComments.Where(c => !c.IsDelete && c.CourseId == courseId).Count() / take;
            if ((pageCount % 2) != 0)
            {
                pageId++;
            }

            return Tuple.Create(_context.CourseComments
                .Include(c => c.User)
                .Where(c => !c.IsDelete && c.CourseId == courseId)
                .Skip(skip)
                .Take(take)
                .OrderByDescending(c => c.CreatedDate)
                .ToList(), pageCount);
        }

        public void AddVote(int userId, int courseId, bool vote)
        {
            var userVote = _context.CourseVotes.FirstOrDefault(c => c.UserId == userId && c.CourseId == courseId);
            if (userVote != null)
            {
                userVote.Vote = vote;
            }
            else
            {
                userVote = new CourseVote()
                {
                    CourseId = courseId,
                    UserId = userId,
                    Vote = vote
                };
                _context.Add(userVote);
            }

            _context.SaveChanges();
        }

        public Tuple<int, int> GetCourseVotes(int courseId)
        {
            var votes = _context.CourseVotes.Where(v => v.CourseId == courseId)
                .Select(v => v.Vote).ToList();

            return Tuple.Create(votes.Count(c => c), votes.Count(c => !c));
        }
    }
}