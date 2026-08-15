using Microsoft.AspNetCore.Mvc;
using JPCS.Models;
using System.Linq;

namespace JPCS.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool emailExists = DataStore.Users.Any(u => u.Email == model.Email);
            if (emailExists)
            {
                ModelState.AddModelError("", "Email is already registered.");
                return View(model);
            }

            var user = new User
            {
                Id = DataStore.GetNextUserId(),
                FullName = model.FullName,
                StudentFacultyId = model.StudentFacultyId,
                Email = model.Email,
                Password = model.Password,
                ContactNumber = model.ContactNumber
            };

            DataStore.Users.Add(user);

            TempData["Success"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = DataStore.Users
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View(model);
            }

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserFullName", user.FullName);

            return RedirectToAction("Dashboard", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login");

            int userId = int.Parse(userIdStr);
            var user = DataStore.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return RedirectToAction("Login");

            var myRentals = DataStore.Rentals
                .Where(r => r.UserId == userId && !r.Returned)
                .Select(r => new RentalDisplay
                {
                    RentalId = r.Id,
                    BookTitle = DataStore.Books.FirstOrDefault(b => b.Id == r.BookId).Title,
                    RentDate = r.RentDate,
                    DueDate = r.DueDate,
                    IsOverdue = r.DueDate < System.DateTime.Now
                })
                .ToList();

            ViewBag.User = user;
            return View(myRentals);
        }

        [HttpPost]
        public IActionResult UpdateProfile(string Email, string Password, string ContactNumber)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login");

            int userId = int.Parse(userIdStr);
            var user = DataStore.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return RedirectToAction("Login");

            if (!string.IsNullOrWhiteSpace(Email)) user.Email = Email;
            if (!string.IsNullOrWhiteSpace(Password)) user.Password = Password;
            if (!string.IsNullOrWhiteSpace(ContactNumber)) user.ContactNumber = ContactNumber;

            HttpContext.Session.SetString("UserEmail", user.Email);
            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction("Profile");
        }
    }
}
