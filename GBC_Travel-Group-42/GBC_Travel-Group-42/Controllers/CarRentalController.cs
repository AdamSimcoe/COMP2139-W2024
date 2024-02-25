using GBC_Travel_Group_42.Data;
using GBC_Travel_Group_42.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Controllers
{
    public class CarRentalController : Controller
    {
        private readonly AppDbContext _db;

        public CarRentalController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchCarMake, int? minRating, decimal? maxPricePerDay)
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

            var allCarRentals = carRentalsQuery.ToList();
            return View(allCarRentals);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var allCarRentals = _db.CarRentals
                .FirstOrDefault(m => m.CarRentalId == id);

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
        public IActionResult Create(CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                _db.CarRentals.Add(carRental);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(carRental);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editCarRental = _db.CarRentals.Find(id);

            if (editCarRental == null)
            {
                return NotFound();
            }
            return View(editCarRental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CarRentalId, CarRentalMake, PricePerDay, Rating, MaximumOccupancy")] CarRental carRental)
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
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarRentalExists(carRental.CarRentalId))
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteCarRental = _db.CarRentals.FirstOrDefault(m => m.CarRentalId == id);

            if (deleteCarRental == null)
            {
                return NotFound();
            }
            return View(deleteCarRental);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleteCarRental = _db.CarRentals.Find(id);

            if (deleteCarRental != null)
            {
                _db.CarRentals.Remove(deleteCarRental);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool CarRentalExists(int id)
        {
            return _db.CarRentals.Any(e => e.CarRentalId == id);
        }
    }
}
