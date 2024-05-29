using D3.Models;
using Microsoft.AspNetCore.Mvc;

namespace D3.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult AddUser()
        {
            if (HttpContext.Session.GetInt32("Id") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if(HttpContext.Session.GetString("Role") != "Super Admin")
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }

            ViewBag.user = CurrentUser.user;
            ViewBag.Students = CurrentUser.Students;
            ViewBag.Users = CurrentUser.Users;

            return View();
        }

        [HttpPost]
        public ActionResult AddUser(string username, string password, string phone, string email, string role)
        {
            User user = new User
            {
                UserName = username,
                Password = password,
                PhoneNumber = phone,
                Email = email,
                Role = role
            };

            if (DbHandler.AddUser(user))
            {
                TempData["SuccessMessage"] = "User added successfully.";
            }

            CurrentUser.Users = DbHandler.GetAllUser(CurrentUser.user.Id);

            return RedirectToAction("AddUser", "User");
        }

        [HttpGet]
        public ActionResult EditUser(User user)
        {
            if (HttpContext.Session.GetInt32("Id") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (HttpContext.Session.GetString("Role") != "Super Admin" || !(DbHandler.CheckUserById(user.Id)))
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }


            ViewBag.user = CurrentUser.user;
            ViewBag.Students = CurrentUser.Students;
            ViewBag.Users = CurrentUser.Users;

            ViewBag.editUser = user;

            return View();
        }

        [HttpPost]
        public ActionResult EditUser(int id, string username, string password, string phone, string email, string role)
        {
            User user = new User
            {
                Id = id,
                UserName = username,
                Password = password,
                PhoneNumber = phone,
                Email = email,
                Role = role
            };

            if (DbHandler.EditUser(user))
            {
                TempData["SuccessMessage"] = "User updated successfully.";
                CurrentUser.Users = DbHandler.GetAllUser(CurrentUser.user.Id);
            }
            else
            {
                TempData["ErrorMessage"] = "Error updating user.";
            }

            TempData["InitialTable"] = "users";
            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}
