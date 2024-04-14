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
    public class HotelController : Controller
    {
        private readonly AppDbContext _db;

        public HotelController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchName, string searchLocation, int? rating, decimal? maxPricePerNight)
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

            var allHotels = await hotelsQuery.ToListAsync();
            return View(allHotels);
        }


        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var allHotels = await _db.Hotels
                .FirstOrDefaultAsync(m => m.HotelId == id);

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
        public async Task<IActionResult> Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                await _db.Hotels.AddAsync(hotel);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var editHotel = await _db.Hotels.FindAsync(id);

            if (editHotel == null)
            {
                return NotFound();
            }
            return View(editHotel);
        }

        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HotelId, HotelName, HotelLocation, PricePerNight, Rating, MaximumOccupancy")] Hotel hotel)
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
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await HotelExists(hotel.HotelId))
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteHotel = await _db.Hotels.FirstOrDefaultAsync(m => m.HotelId == id);

            if (deleteHotel == null)
            {
                return NotFound();
            }
            return View(deleteHotel);
        }

        [HttpPost, ActionName("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteHotel = await _db.Hotels.FindAsync(id);

            if (deleteHotel != null)
            {
                _db.Hotels.Remove(deleteHotel);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _db.Hotels.AnyAsync(e => e.HotelId == id);
        }
    }
}
