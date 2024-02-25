using GBC_Travel_Group_42.Data;
using GBC_Travel_Group_42.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Controllers
{
    public class FlightController : Controller
    {
        private readonly AppDbContext _db;

        public FlightController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchQuery, int? rating, decimal? price)
        {
            var flightsQuery = from m in _db.Flights
                              select m;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                flightsQuery = flightsQuery.Where(s => s.Origin.Contains(searchQuery) || s.Destination.Contains(searchQuery)); 
            }

            if (price.HasValue)
            {
                flightsQuery = flightsQuery.Where(x => x.Price <= price.Value);
            }

            if (rating.HasValue)
            {
                flightsQuery = flightsQuery.Where(x => x.Rating >= rating.Value);
            }

            var allFlights = flightsQuery.ToList();
            return View(allFlights);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var allFlights = _db.Flights
                .FirstOrDefault(m => m.FlightId == id);

            if (allFlights == null)
            {
                return NotFound();
            }

            return View(allFlights);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Flight flight)
        {
            if (ModelState.IsValid)
            { 
                _db.Flights.Add(flight);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editFlight = _db.Flights.Find(id);

            if (editFlight == null)
            {
                return NotFound();
            }
            return View(editFlight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FlightId, FlightNumber, Origin, Destination, DepartureDateTime, ArrivalDateTime, Price, Rating, MaximumOccupancy")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {  
                try
                {
                    _db.Update(flight);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingFlightExists(flight.FlightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteFlight = _db.Flights.FirstOrDefault(m => m.FlightId == id);

            if (deleteFlight == null)
            {
                return NotFound();
            }
            return View(deleteFlight);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleteFlight = _db.Flights.Find(id);

            if (deleteFlight != null)
            {
                _db.Flights.Remove(deleteFlight);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool BookingFlightExists(int id)
        {
            return _db.Flights.Any(e => e.FlightId == id);
        }
    }
}
