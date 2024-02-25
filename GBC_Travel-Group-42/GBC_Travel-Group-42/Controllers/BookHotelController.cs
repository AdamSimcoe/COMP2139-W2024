using GBC_Travel_Group_42.Data;
using GBC_Travel_Group_42.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Controllers
{
    public class BookHotelController : Controller
    {
        private readonly AppDbContext _db;

        public BookHotelController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allHotelBookings = _db.BookingHotels.Include(bh => bh.Hotel).ToList();
            return View(allHotelBookings);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var allHotelBookings = _db.BookingHotels.Include(bh =>bh.Hotel).FirstOrDefault(bh => bh.BookingHotelId == id);
            
            if (allHotelBookings == null)
            {
                return NotFound();
            }

            return View(allHotelBookings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GuestFirstName, GuestLastName, GuestEmail, Accommodations, HotelId, CheckInDateTime, ExpectedStayDuration")] BookHotel bookHotel)
        {
            var hotel = _db.Hotels.FirstOrDefault(h => h.HotelId == bookHotel.HotelId);

            if (hotel != null)
            {
                int currentBookings = _db.BookingHotels.Count(bh => bh.HotelId == bookHotel.HotelId);

                if (currentBookings >= hotel.MaximumOccupancy)
                {
                    ModelState.AddModelError("", "This hotel is already fully booked, please choose another hotel.");
                    ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName", bookHotel.HotelId);
                    return View(bookHotel);
                }
            }

            if (ModelState.IsValid)
            {
                bookHotel.CheckOutDateTime = bookHotel.CheckInDateTime.AddDays(bookHotel.ExpectedStayDuration);

                var selectedHotel = _db.Hotels.FirstOrDefault(h => h.HotelId == bookHotel.HotelId);

                if (selectedHotel != null)
                {
                    bookHotel.HotelTotalPrice = selectedHotel.PricePerNight * bookHotel.ExpectedStayDuration;
                }

                _db.BookingHotels.Add(bookHotel);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName", bookHotel.HotelId);
            return View(bookHotel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editHotelBooking = _db.BookingHotels.Find(id);
            
            if (editHotelBooking == null)
            {
                return NotFound();
            }

            ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName", editHotelBooking.HotelId);
            return View(editHotelBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookingHotelId, GuestFirstName, GuestLastName, GuestEmail, Accommodations, HotelId, CheckInDateTime, ExpectedStayDuration")] BookHotel bookHotel)
        {
            if (id != bookHotel.BookingHotelId)
            {
                return NotFound();
            }

            var hotel = _db.Hotels.FirstOrDefault(h => h.HotelId == bookHotel.HotelId);

            if (hotel != null)
            {
                int currentBookings = _db.BookingHotels.Count(bh => bh.HotelId == bookHotel.HotelId);

                if (currentBookings >= hotel.MaximumOccupancy)
                {
                    ModelState.AddModelError("", "This hotel is already fully booked, please choose another hotel.");
                    ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName", bookHotel.HotelId);
                    return View(bookHotel);
                }
            }

            if (ModelState.IsValid)
            {
                bookHotel.CheckOutDateTime = bookHotel.CheckInDateTime.AddDays(bookHotel.ExpectedStayDuration);

                var selectedHotel = _db.Hotels.FirstOrDefault(h => h.HotelId == bookHotel.HotelId);

                if (selectedHotel != null)
                {
                    bookHotel.HotelTotalPrice = selectedHotel.PricePerNight * bookHotel.ExpectedStayDuration;
                }

                _db.Update(bookHotel);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName", bookHotel.HotelId);
            return View(bookHotel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteHotelBooking = _db.BookingHotels.Include(bh => bh.Hotel).FirstOrDefault(bh => bh.BookingHotelId == id);

            if (deleteHotelBooking == null)
            {
                return NotFound();
            }

            return View(deleteHotelBooking);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleteHotelBooking = _db.BookingHotels.Find(id);

            if (deleteHotelBooking != null)
            {
                _db.BookingHotels.Remove(deleteHotelBooking);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

    }
}
