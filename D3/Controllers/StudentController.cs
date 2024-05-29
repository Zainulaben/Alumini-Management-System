using D3.Models;
using Microsoft.AspNetCore.Mvc;

namespace D3.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult AddStudent()
        {
            if(HttpContext.Session.GetInt32("Id") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if(HttpContext.Session.GetString("Role") == "Clerk")
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }

            ViewBag.user = CurrentUser.user;
            ViewBag.Students = CurrentUser.Students;
            ViewBag.Users = CurrentUser.Users;

            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(string username, string email, string session, string rollno)
        {
            Student student = new Student
            {
                UserName = username,
                Email = email,
                Session = session,
                RollNumber = rollno
            };

            if (DbHandler.AddStudent(student))
            {
                TempData["SuccessMessage"] = "Student added successfully.";
            }

            CurrentUser.Students = DbHandler.GetAllStudents();

            return RedirectToAction("AddStudent", "Student");
        }

        [HttpGet]
        public ActionResult EditStudent(Student student)
        {
            if (HttpContext.Session.GetInt32("Id") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (HttpContext.Session.GetString("Role") == "Clerk" || !(DbHandler.CheckStudentById(student.Id)))
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }


            ViewBag.user = CurrentUser.user;
            ViewBag.Students = CurrentUser.Students;
            ViewBag.Users = CurrentUser.Users;

            ViewBag.editStudent = student;

            return View();
        }

        [HttpPost]
        public ActionResult EditStudent(int id, string username, string email, string session, string rollno)
        {
            Student student = new Student
            {
                Id = id,
                UserName = username,
                Email = email,
                Session = session,
                RollNumber = rollno
            };

            if (DbHandler.EditStudent(student))
            {
                TempData["SuccessMessage"] = "Student updated successfully.";
                CurrentUser.Students = DbHandler.GetAllStudents();
            }
            else
            {
                TempData["ErrorMessage"] = "Error updating student.";
            }

            TempData["InitialTable"] = "students";
            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}
