using D3.Models;
using Microsoft.AspNetCore.Mvc;

namespace D3.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Dashboard(string initialTable = "users")
        {
            if (HttpContext.Session.GetInt32("Id") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.user = CurrentUser.user;
            ViewBag.Students = CurrentUser.Students;
            ViewBag.Users = CurrentUser.Users;
            ViewBag.InitialTable = initialTable;

            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            if (DbHandler.CheckUserById(id) && DbHandler.DeleteUser(id))
            {
                TempData["SuccessMessage"] = "User deleted successfully.";
                CurrentUser.Users = DbHandler.GetAllUser(CurrentUser.user.Id);
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting user.";
            }

            TempData["InitialTable"] = "users";
            return RedirectToAction("Dashboard", "Dashboard");
        }

        [HttpPost]
        public ActionResult DeleteStudent(int id)
        {
            if (DbHandler.CheckStudentById(id) && DbHandler.DeleteStudent(id))
            {
                TempData["SuccessMessage"] = "Student deleted successfully.";
                CurrentUser.Students = DbHandler.GetAllStudents();
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting student.";
            }

            TempData["InitialTable"] = "students";
            return RedirectToAction("Dashboard", "Dashboard");
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            User? user = DbHandler.GetUserById(id);

            if (user != null)
            {
                return RedirectToAction("EditUser", "User", user);
            }

            return RedirectToAction("Dashboard", "Dashboard");
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            Student? student = DbHandler.GetStudentById(id);

            if (student != null)
            {
                return RedirectToAction("EditStudent", "Student", student);
            }

            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}
