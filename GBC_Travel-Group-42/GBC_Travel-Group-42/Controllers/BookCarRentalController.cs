using GBC_Travel_Group_42.Data;
using GBC_Travel_Group_42.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Controllers
{
    public class BookCarRentalController : Controller
    {
        private readonly AppDbContext _db;

        public BookCarRentalController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allCarRentalBookings = _db.BookingCarRentals.Include(bc => bc.CarRental).ToList();
            return View(allCarRentalBookings);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var allCarRentalBookings = _db.BookingCarRentals.Include(bc => bc.CarRental).FirstOrDefault(bc => bc.BookingCarRentalId == id);

            if (allCarRentalBookings == null)
            {
                return NotFound();
            }

            return View(allCarRentalBookings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GuestFirstName, GuestLastName, GuestEmail, Accommodations, CarRentalId, RentalPickupDateTime, RentalDuration")] BookCarRental bookCarRental)
        {
            var carRental = _db.CarRentals.FirstOrDefault(cr => cr.CarRentalId == bookCarRental.CarRentalId);

            if (carRental != null)
            {
                int currentBookings = _db.BookingCarRentals.Count(bc => bc.CarRentalId == bookCarRental.CarRentalId);

                if (currentBookings >= carRental.MaximumOccupancy)
                {
                    ModelState.AddModelError("", "This car rental is already fully booked, please choose another car rental.");
                    ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", bookCarRental.CarRentalId);
                    return View(bookCarRental);
                }
            }

            if (ModelState.IsValid)
            {
                bookCarRental.RentalReturnDateTime = bookCarRental.RentalPickupDateTime.AddDays(bookCarRental.RentalDuration);

                var selectedCarRental = _db.CarRentals.FirstOrDefault(cr => cr.CarRentalId == bookCarRental.CarRentalId);

                if (selectedCarRental != null)
                {
                    bookCarRental.RentalTotalPrice = selectedCarRental.PricePerDay * bookCarRental.RentalDuration;
                }

                _db.BookingCarRentals.Add(bookCarRental);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", bookCarRental.CarRentalId);
            return View(bookCarRental);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editCarRentalBooking = _db.BookingCarRentals.Find(id);

            if (editCarRentalBooking == null)
            {
                return NotFound();
            }

            ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", editCarRentalBooking.CarRentalId);
            return View(editCarRentalBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookingCarRentalId, GuestFirstName, GuestLastName, GuestEmail, Accommodations, CarRentalId, RentalPickupDateTime, RentalDuration")] BookCarRental bookCarRental)
        {
            if (id != bookCarRental.BookingCarRentalId)
            {
                return NotFound();
            }

            var carRental = _db.CarRentals.FirstOrDefault(cr => cr.CarRentalId == bookCarRental.CarRentalId);

            if (carRental != null)
            {
                int currentBookings = _db.BookingCarRentals.Count(bc => bc.CarRentalId == bookCarRental.CarRentalId);

                if (currentBookings >= carRental.MaximumOccupancy)
                {
                    ModelState.AddModelError("", "This car rental is already fully booked, please choose another car rental.");
                    ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", bookCarRental.CarRentalId);
                    return View(bookCarRental);
                }
            }

            if (ModelState.IsValid)
            {
                bookCarRental.RentalReturnDateTime = bookCarRental.RentalPickupDateTime.AddDays(bookCarRental.RentalDuration);

                var selectedCarRental = _db.CarRentals.FirstOrDefault(cr => cr.CarRentalId == bookCarRental.CarRentalId);

                if (selectedCarRental != null)
                {
                    bookCarRental.RentalTotalPrice = selectedCarRental.PricePerDay * bookCarRental.RentalDuration;
                }

                _db.Update(bookCarRental);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", bookCarRental.CarRentalId);
            return View(bookCarRental);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteCarRentalBooking = _db.BookingCarRentals.Include(bc => bc.CarRental).FirstOrDefault(bc => bc.BookingCarRentalId == id);

            if (deleteCarRentalBooking == null)
            {
                return NotFound();
            }

            return View(deleteCarRentalBooking);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleteCarRentalBooking = _db.BookingCarRentals.Find(id);

            if (deleteCarRentalBooking != null)
            {
                _db.BookingCarRentals.Remove(deleteCarRentalBooking);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}

