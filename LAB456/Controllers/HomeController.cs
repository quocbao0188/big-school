using LAB456.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LAB456.ViewModels;
using Microsoft.AspNet.Identity;

namespace LAB456.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }

        //public ActionResult Index()
        //{
        //    var upcommingCourses = _dbContext.Courses
        //        .Include(c => c.Lecturer)
        //        .Include(c => c.Category)
        //        .Where(c => c.DateTime > DateTime.Now);

        //    var viewModel = new CoursesViewModel
        //    {
        //        UpcommingCourses = upcommingCourses,
        //        ShowAction = User.Identity.IsAuthenticated
        //    };
        //    return View(viewModel);
        //}
        public ActionResult Index(string searching)
        {
            var viewModel = new CoursesViewModel();
            if (searching == null)
            {
                var userId = User.Identity.GetUserId();
                var listOfAttendedCourses = _dbContext.Attendances
                    .Include(a => a.Course)
                    .Include(a => a.Attendee)
                    .Where(a => a.AttendeeId == userId).ToList();

                var upCommingCourses = _dbContext.Courses
                    .Include(c => c.Lecturer)
                    .Include(c => c.Category)
                    .Where(c => c.DateTime > DateTime.Now).ToList();

                var followingLecturers = _dbContext.Following
                    .Include(f => f.Followee)
                    .Include(f => f.Follower)
                    .Where(a => a.FollowerId == userId)
                    .ToList();

                viewModel = new CoursesViewModel
                {
                    ListOfAttendedCourses = listOfAttendedCourses,
                    ListOfFollowings = followingLecturers,
                    UpcommingCourses = upCommingCourses,
                    ShowAction = User.Identity.IsAuthenticated
                };
                return View(viewModel);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var listOfAttendedCourses = _dbContext.Attendances
                    .Include(a => a.Course)
                    .Include(a => a.Attendee)
                    .Where(a => a.AttendeeId == userId).ToList();

                var upCommingCourses = _dbContext.Courses
                    .Include(c => c.Lecturer)
                    .Include(c => c.Category)
                    .Where(c => c.Lecturer.Name.Contains(searching) || c.Category.Name.Contains(searching)).ToList();

                var followingLecturers = _dbContext.Following
                    .Include(f => f.Followee)
                    .Include(f => f.Follower)
                    .Where(a => a.FollowerId == userId)
                    .ToList();

                viewModel = new CoursesViewModel
                {
                    ListOfAttendedCourses = listOfAttendedCourses,
                    ListOfFollowings = followingLecturers,
                    UpcommingCourses = upCommingCourses,
                    ShowAction = User.Identity.IsAuthenticated
                };
                return View(viewModel);
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}