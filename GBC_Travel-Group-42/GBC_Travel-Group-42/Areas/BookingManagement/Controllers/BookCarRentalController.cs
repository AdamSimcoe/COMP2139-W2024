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
    public class BookCarRentalController : Controller
    {
        private readonly AppDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public BookCarRentalController(AppDbContext db, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _db = db;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCarRentalBookings = await _db.BookingCarRentals.Include(bc => bc.CarRental).ToListAsync();
            return View(allCarRentalBookings);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var allCarRentalBookings = await _db.BookingCarRentals.Include(bc => bc.CarRental).FirstOrDefaultAsync(bc => bc.BookingCarRentalId == id);

            if (allCarRentalBookings == null)
            {
                return NotFound();
            }

            return View(allCarRentalBookings);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create(int? carRentalId)
        {
            var model = new BookCarRental()
            {
                RentalPickupDateTime = DateTime.Today,
            };

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                model.GuestFirstName = user.FirstName;
                model.GuestLastName = user.LastName;
                model.GuestEmail = user.Email;
                model.UserId = user.Id;
            }

            ViewBag.CarRentals = carRentalId.HasValue ?
                new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", carRentalId) :
                new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", carRentalId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("GuestFirstName, GuestLastName, GuestEmail, Accommodations, CarRentalId, RentalPickupDateTime, RentalDuration")] BookCarRental bookCarRental)
        {
            if (User.Identity.IsAuthenticated)
            {
                bookCarRental.UserId = _userManager.GetUserId(User);
            }

            var carRental = await _db.CarRentals.FirstOrDefaultAsync(cr => cr.CarRentalId == bookCarRental.CarRentalId);

            if (carRental != null)
            {
                int currentBookings = await _db.BookingCarRentals.CountAsync(bc => bc.CarRentalId == bookCarRental.CarRentalId);

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

                var selectedCarRental = await _db.CarRentals.FirstOrDefaultAsync(cr => cr.CarRentalId == bookCarRental.CarRentalId);

                if (selectedCarRental != null)
                {
                    bookCarRental.RentalTotalPrice = selectedCarRental.PricePerDay * bookCarRental.RentalDuration;
                }

                await _db.BookingCarRentals.AddAsync(bookCarRental);
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

            ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", bookCarRental.CarRentalId);
            return View(bookCarRental);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var editCarRentalBooking = await _db.BookingCarRentals.FindAsync(id);

            if (editCarRentalBooking == null)
            {
                return NotFound();
            }

            ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", editCarRentalBooking.CarRentalId);
            return View(editCarRentalBooking);
        }

        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingCarRentalId, GuestFirstName, GuestLastName, GuestEmail, Accommodations, CarRentalId, RentalPickupDateTime, RentalDuration")] BookCarRental bookCarRental)
        {
            if (id != bookCarRental.BookingCarRentalId)
            {
                return NotFound();
            }

            var carRental = await _db.CarRentals.FirstOrDefaultAsync(cr => cr.CarRentalId == bookCarRental.CarRentalId);

            if (carRental != null)
            {
                int currentBookings = await _db.BookingCarRentals.CountAsync(bc => bc.CarRentalId == bookCarRental.CarRentalId);

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

                var selectedCarRental = await _db.CarRentals.FirstOrDefaultAsync(cr => cr.CarRentalId == bookCarRental.CarRentalId);

                if (selectedCarRental != null)
                {
                    bookCarRental.RentalTotalPrice = selectedCarRental.PricePerDay * bookCarRental.RentalDuration;
                }

                _db.Update(bookCarRental);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarRentals = new SelectList(_db.CarRentals, "CarRentalId", "CarRentalMake", bookCarRental.CarRentalId);
            return View(bookCarRental);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteCarRentalBooking = await _db.BookingCarRentals.Include(bc => bc.CarRental).FirstOrDefaultAsync(bc => bc.BookingCarRentalId == id);

            if (deleteCarRentalBooking == null)
            {
                return NotFound();
            }

            return View(deleteCarRentalBooking);
        }

        [HttpPost, ActionName("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteCarRentalBooking = await _db.BookingCarRentals.FindAsync(id);

            if (deleteCarRentalBooking != null)
            {
                _db.BookingCarRentals.Remove(deleteCarRentalBooking);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}

