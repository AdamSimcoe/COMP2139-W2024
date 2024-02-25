using GBC_Travel_Group_42.Data;
using GBC_Travel_Group_42.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Controllers
{
    public class BookFlightController : Controller
    {
        private readonly AppDbContext _db;

        public BookFlightController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allFlightBookings = _db.BookingFlights.Include(bf => bf.Flight).ToList();
            return View(allFlightBookings);
        }

        [HttpGet] 
        public IActionResult Details(int id)
        {
            var allFlightBookings = _db.BookingFlights.Include(_bf => _bf.Flight).FirstOrDefault(bf => bf.BookingFlightId == id);

            if (allFlightBookings == null)
            {
                return NotFound();
            }

            return View(allFlightBookings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GuestFirstName, GuestLastName, GuestEmail, Accommodations, FlightId")] BookFlight bookFlight)
        {
            var flight = _db.Flights.FirstOrDefault(f => f.FlightId == bookFlight.FlightId);

            if (flight != null)
            {
                int currentBookings = _db.BookingFlights.Count(bf => bf.FlightId == bookFlight.FlightId);

                if (currentBookings >= flight.MaximumOccupancy)
                {
                    ModelState.AddModelError("", "This flight is already fully booked, please choose another flight.");
                    ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", bookFlight.FlightId);
                    return View(bookFlight);
                }
            }

            if (ModelState.IsValid)
            {
                _db.BookingFlights.Add(bookFlight);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", bookFlight.FlightId);
            return View(bookFlight);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editFlightBooking = _db.BookingFlights.Find(id);

            if (editFlightBooking == null)
            {
                return NotFound();
            }

            ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", editFlightBooking.FlightId);
            return View(editFlightBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookingFlightId, GuestFirstName, GuestLastName, GuestEmail, Accommodations, FlightId")] BookFlight bookFlight)
        {
            if (id != bookFlight.BookingFlightId)
            {
                return NotFound();
            }

            var flight = _db.Flights.FirstOrDefault(f => f.FlightId == bookFlight.FlightId);

            if (flight != null)
            {
                int currentBookings = _db.BookingFlights.Count(bf => bf.FlightId == bookFlight.FlightId);

                if (currentBookings >= flight.MaximumOccupancy)
                {
                    ModelState.AddModelError("", "This flight is already fully booked, please choose another flight.");
                    ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", bookFlight.FlightId);
                    return View(bookFlight);
                }
            }

            if (ModelState.IsValid)
            {
                _db.Update(bookFlight);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", bookFlight.FlightId);
            return View(bookFlight);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteFlightBooking = _db.BookingFlights.Include(bf => bf.Flight).FirstOrDefault(bf => bf.BookingFlightId == id);

            if (deleteFlightBooking == null)
            {
                return NotFound();
            }

            return View(deleteFlightBooking);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleteFlightBooking = _db.BookingFlights.Find(id);

            if (deleteFlightBooking != null)
            {
                _db.BookingFlights.Remove(deleteFlightBooking);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
