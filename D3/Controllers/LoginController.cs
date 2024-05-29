using D3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D3.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Id") != null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            return View();

        }

        [HttpPost]
        public ActionResult CheckUser(string username, string password)
        {
            User user = DbHandler.CheckUser(username, password);

            if (user == null)
            {
                ViewBag.Message = "Username or Password are Incorrect.";
                return View("Index");
            }

            HttpContext.Session.SetInt32("Id", user.Id);
            HttpContext.Session.SetString("Role", user.Role);

            CurrentUser.user = user;
            CurrentUser.Students = DbHandler.GetAllStudents();
            CurrentUser.Users = DbHandler.GetAllUser(user.Id);

            return RedirectToAction("Dashboard", "Dashboard");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("Role");
            return RedirectToAction("Index", "Login");
        }
    }
}
