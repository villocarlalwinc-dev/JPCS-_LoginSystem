using Microsoft.AspNetCore.Mvc;
using JPCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JPCS.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index(string search)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdStr);

            var books = DataStore.Books.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                books = books.Where(b =>
                    b.Title.ToLower().Contains(search.ToLower()) ||
                    b.Author.ToLower().Contains(search.ToLower()));
            }

            ViewBag.Search = search;
            ViewBag.MyActiveRentals = DataStore.Rentals.Count(r => r.UserId == userId && !r.Returned);
            ViewBag.RentedBookIds = DataStore.Rentals.Where(r => !r.Returned).Select(r => r.BookId).ToList();

            return View(books.ToList());
        }

        [HttpPost]
        public IActionResult Rent(int id)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdStr);

            bool alreadyRentedByAnyone = DataStore.Rentals.Any(r => r.BookId == id && !r.Returned);
            int myActiveCount = DataStore.Rentals.Count(r => r.UserId == userId && !r.Returned);

            if (alreadyRentedByAnyone)
            {
                TempData["Error"] = "This book is currently unavailable.";
            }
            else if (myActiveCount >= 3)
            {
                TempData["Error"] = "You already have 3 books rented. Please return one before renting another.";
            }
            else
            {
                DataStore.Rentals.Add(new Rental
                {
                    Id = DataStore.GetNextRentalId(),
                    UserId = userId,
                    BookId = id,
                    RentDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7)
                });
                TempData["Success"] = "Book rented successfully!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Return(int rentalId)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            var rental = DataStore.Rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental != null) rental.Returned = true;

            TempData["Success"] = "Book returned.";
            return RedirectToAction("Profile", "Account");
        }
    }
}
