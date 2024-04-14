using GBC_Travel_Group_42.Areas.BookingManagement.Models;
using GBC_Travel_Group_42.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Areas.BookingManagement.Controllers
{
    [Authorize(Policy = "StaffAccess")]
    [Area("BookingManagement")]
    [Route("[area]/[controller]/[action]")]
    public class BookHotelController : Controller
    {
        private readonly AppDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public BookHotelController(AppDbContext db, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _db = db;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allHotelBookings = await _db.BookingHotels.Include(bh => bh.Hotel).ToListAsync();
            return View(allHotelBookings);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var allHotelBookings = await _db.BookingHotels.Include(bh => bh.Hotel).FirstOrDefaultAsync(bh => bh.BookingHotelId == id);

            if (allHotelBookings == null)
            {
                return NotFound();
            }

            return View(allHotelBookings);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create(int? hotelid)
        {
            var model = new BookHotel()
            {
                CheckInDateTime = DateTime.Today,
            };

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                model.GuestFirstName = user.FirstName;
                model.GuestLastName = user.LastName;
                model.GuestEmail = user.Email;
                model.UserId = user.Id;
            }

            ViewBag.Hotels = hotelid.HasValue ?
                new SelectList(_db.Hotels, "HotelId", "HotelName", hotelid) :
                new SelectList(_db.Hotels, "HotelId", "HotelName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("GuestFirstName, GuestLastName, GuestEmail, Accommodations, HotelId, CheckInDateTime, ExpectedStayDuration")] BookHotel bookHotel)
        {
			if (User.Identity.IsAuthenticated)
			{
				bookHotel.UserId = _userManager.GetUserId(User);
			}

			var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.HotelId == bookHotel.HotelId);

            if (hotel != null)
            {
                int currentBookings = await _db.BookingHotels.CountAsync(bh => bh.HotelId == bookHotel.HotelId);

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

                var selectedHotel = await _db.Hotels.FirstOrDefaultAsync(h => h.HotelId == bookHotel.HotelId);

                if (selectedHotel != null)
                {
                    bookHotel.HotelTotalPrice = selectedHotel.PricePerNight * bookHotel.ExpectedStayDuration;
                }

                await _db.BookingHotels.AddAsync(bookHotel);
                await _db.SaveChangesAsync();

                var isStaff = await _authorizationService.AuthorizeAsync(User, "StaffAccess");
                if (isStaff.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "MyBookings", new { area = "BookingManagement" });
                }
                else
                {
                    TempData["ConfirmationMessage"] = "Your booking has been successfully added!";
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName", bookHotel.HotelId);
            return View(bookHotel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var editHotelBooking = await _db.BookingHotels.FindAsync(id);

            if (editHotelBooking == null)
            {
                return NotFound();
            }

            ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName", editHotelBooking.HotelId);
            return View(editHotelBooking);
        }

        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingHotelId, GuestFirstName, GuestLastName, GuestEmail, Accommodations, HotelId, CheckInDateTime, ExpectedStayDuration")] BookHotel bookHotel)
        {
            if (id != bookHotel.BookingHotelId)
            {
                return NotFound();
            }

            var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.HotelId == bookHotel.HotelId);

            if (hotel != null)
            {
                int currentBookings = await _db.BookingHotels.CountAsync(bh => bh.HotelId == bookHotel.HotelId);

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
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hotels = new SelectList(_db.Hotels, "HotelId", "HotelName", bookHotel.HotelId);
            return View(bookHotel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteHotelBooking = await _db.BookingHotels.Include(bh => bh.Hotel).FirstOrDefaultAsync(bh => bh.BookingHotelId == id);

            if (deleteHotelBooking == null)
            {
                return NotFound();
            }

            return View(deleteHotelBooking);
        }

        [HttpPost, ActionName("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteHotelBooking = await _db.BookingHotels.FindAsync(id);

            if (deleteHotelBooking != null)
            {
                _db.BookingHotels.Remove(deleteHotelBooking);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

    }
}
