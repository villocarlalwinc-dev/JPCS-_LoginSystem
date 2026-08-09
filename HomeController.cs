using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JPCS.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JPCS.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Protect this page - must be logged in to view it
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
                return RedirectToAction("Login", "Account");

            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");

            var announcements = await _context.Announcements
                .OrderByDescending(a => a.DatePosted)
                .ToListAsync();

            return View(announcements);
        }
    }
}
