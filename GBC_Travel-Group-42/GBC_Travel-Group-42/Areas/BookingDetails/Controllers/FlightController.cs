using GBC_Travel_Group_42.Areas.BookingDetails.Models;
using GBC_Travel_Group_42.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Areas.BookingDetails.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    [Area("BookingDetails")]
    [Route("[area]/[controller]/[action]")]
    public class FlightController : Controller
    {
        private readonly AppDbContext _db;

        public FlightController(AppDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchQuery, int? rating, decimal? price)
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

            var allFlights =  await flightsQuery.ToListAsync();
            return View(allFlights);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var allFlights = await _db.Flights
                .FirstOrDefaultAsync(m => m.FlightId == id);

            if (allFlights == null)
            {
                return NotFound();
            }

            return View(allFlights);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                await _db.Flights.AddAsync(flight);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var editFlight = await _db.Flights.FindAsync(id);

            if (editFlight == null)
            {
                return NotFound();
            }
            return View(editFlight);
        }

        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId, FlightNumber, Origin, Destination, DepartureDateTime, ArrivalDateTime, Price, Rating, MaximumOccupancy")] Flight flight)
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
                   await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookingFlightExists(flight.FlightId))
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteFlight = await _db.Flights.FirstOrDefaultAsync(m => m.FlightId == id);

            if (deleteFlight == null)
            {
                return NotFound();
            }
            return View(deleteFlight);
        }

        [HttpPost, ActionName("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteFlight = await _db.Flights.FindAsync(id);

            if (deleteFlight != null)
            {
                _db.Flights.Remove(deleteFlight);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private async Task<bool> BookingFlightExists(int id)
        {
            return await _db.Flights.AnyAsync(e => e.FlightId == id);
        }
    }
}
