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
    public class CarRentalController : Controller
    {
        private readonly AppDbContext _db;

        public CarRentalController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchCarMake, int? minRating, decimal? maxPricePerDay)
        {
            var carRentalsQuery = from c in _db.CarRentals
                                  select c;

            if (!string.IsNullOrEmpty(searchCarMake))
            {
                carRentalsQuery = carRentalsQuery.Where(s => s.CarRentalMake.Contains(searchCarMake));
            }

            if (minRating.HasValue)
            {
                carRentalsQuery = carRentalsQuery.Where(x => x.Rating >= minRating.Value);
            }

            if (maxPricePerDay.HasValue)
            {
                carRentalsQuery = carRentalsQuery.Where(x => x.PricePerDay <= maxPricePerDay.Value);
            }

            var allCarRentals = await carRentalsQuery.ToListAsync();
            return View(allCarRentals);
        }


        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var allCarRentals = await _db.CarRentals
                .FirstOrDefaultAsync(m => m.CarRentalId == id);

            if (allCarRentals == null)
            {
                return NotFound();
            }

            return View(allCarRentals);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                await _db.CarRentals.AddAsync(carRental);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carRental);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var editCarRental = await _db.CarRentals.FindAsync(id);

            if (editCarRental == null)
            {
                return NotFound();
            }
            return View(editCarRental);
        }

        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarRentalId, CarRentalMake, PricePerDay, Rating, MaximumOccupancy")] CarRental carRental)
        {
            if (id != carRental.CarRentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(carRental);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarRentalExists(carRental.CarRentalId))
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
            return View(carRental);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteCarRental = await _db.CarRentals.FirstOrDefaultAsync(m => m.CarRentalId == id);

            if (deleteCarRental == null)
            {
                return NotFound();
            }
            return View(deleteCarRental);
        }

        [HttpPost, ActionName("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteCarRental = await _db.CarRentals.FindAsync(id);

            if (deleteCarRental != null)
            {
                _db.CarRentals.Remove(deleteCarRental);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private async Task<bool> CarRentalExists(int id)
        {
            return await _db.CarRentals.AnyAsync(e => e.CarRentalId == id);
        }
    }
}
