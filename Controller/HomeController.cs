using Microsoft.AspNetCore.Mvc;
using JPCS.Models;
using System.Linq;

namespace JPCS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
                return RedirectToAction("Login", "Account");

            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");

            var announcements = DataStore.Announcements
                .OrderByDescending(a => a.DatePosted)
                .ToList();

            return View(announcements);
        }
    }
}
