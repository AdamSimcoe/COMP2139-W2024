using GBC_Travel_Group_42.Data;
using GBC_Travel_Group_42.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Controllers
{
    public class HotelController : Controller
    {
        private readonly AppDbContext _db;

        public HotelController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchName, string searchLocation, int? rating, decimal? maxPricePerNight)
        {
            var hotelsQuery = from h in _db.Hotels
                         select h;

            if (!string.IsNullOrEmpty(searchName))
            {
                hotelsQuery = hotelsQuery.Where(s => s.HotelName.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchLocation))
            {
                hotelsQuery = hotelsQuery.Where(s => s.HotelLocation.Contains(searchLocation));
            }

            if (maxPricePerNight.HasValue)
            {
                hotelsQuery = hotelsQuery.Where(x => x.PricePerNight <= maxPricePerNight.Value);
            }

            if (rating.HasValue)
            {
                hotelsQuery = hotelsQuery.Where(x => x.Rating >= rating.Value);
            }

            var allHotels = hotelsQuery.ToList();
            return View(allHotels);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var allHotels = _db.Hotels
                .FirstOrDefault(m => m.HotelId == id);

            if (allHotels == null)
            {
                return NotFound();
            }

            return View(allHotels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _db.Hotels.Add(hotel);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editHotel = _db.Hotels.Find(id);

            if (editHotel == null)
            {
                return NotFound();
            }
            return View(editHotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("HotelId, HotelName, HotelLocation, PricePerNight, Rating, MaximumOccupancy")] Hotel hotel)
        {
            if (id != hotel.HotelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(hotel);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.HotelId))
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
            return View(hotel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteHotel = _db.Hotels.FirstOrDefault(m => m.HotelId == id);

            if (deleteHotel == null)
            {
                return NotFound();
            }
            return View(deleteHotel);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleteHotel = _db.Hotels.Find(id);

            if (deleteHotel != null)
            {
                _db.Hotels.Remove(deleteHotel);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool HotelExists(int id)
        {
            return _db.Hotels.Any(e => e.HotelId == id);
        }
    }
}
