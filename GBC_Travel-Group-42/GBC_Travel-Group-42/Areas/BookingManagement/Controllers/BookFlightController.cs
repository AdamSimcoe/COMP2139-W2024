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
    public class BookFlightController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;


		public BookFlightController(AppDbContext db, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _db = db;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allFlightBookings = await _db.BookingFlights.Include(bf => bf.Flight).ToListAsync();
            return View(allFlightBookings);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var allFlightBookings = await _db.BookingFlights.Include(_bf => _bf.Flight).FirstOrDefaultAsync(bf => bf.BookingFlightId == id);

            if (allFlightBookings == null)
            {
                return NotFound();
            }

            return View(allFlightBookings);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create(int? flightId)
        {
            var model = new BookFlight();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                model.GuestFirstName = user.FirstName;
                model.GuestLastName = user.LastName;
                model.GuestEmail = user.Email;
                model.UserId = user.Id;
            }

            ViewBag.Flights = flightId.HasValue ?
                new SelectList(_db.Flights, "FlightId", "FlightNumber", flightId) :
                new SelectList(_db.Flights, "FlightId", "FlightNumber");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("GuestFirstName, GuestLastName, GuestEmail, Accommodations, FlightId")] BookFlight bookFlight)
        {
            if (User.Identity.IsAuthenticated)
            {
                bookFlight.UserId = _userManager.GetUserId(User);
            }

            var flight = await _db.Flights.FirstOrDefaultAsync(f => f.FlightId == bookFlight.FlightId);
            if (flight != null)
            {
                int currentBookings = await _db.BookingFlights.CountAsync(bf => bf.FlightId == bookFlight.FlightId);

                if (currentBookings >= flight.MaximumOccupancy)
                {
                    ModelState.AddModelError("", "This flight is already fully booked, please choose another flight.");
                    ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", bookFlight.FlightId);
                    return View(bookFlight);
                }
            }
            else
            {
                ModelState.AddModelError("", "Flight not found.");
                ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", bookFlight.FlightId);
                return View(bookFlight);
            }

            if (ModelState.IsValid)
            {
                await _db.BookingFlights.AddAsync(bookFlight);
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

            ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", bookFlight.FlightId);
            return View(bookFlight);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var editFlightBooking = await _db.BookingFlights.FindAsync(id);

            if (editFlightBooking == null)
            {
                return NotFound();
            }

            ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", editFlightBooking.FlightId);
            return View(editFlightBooking);
        }

        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingFlightId, GuestFirstName, GuestLastName, GuestEmail, Accommodations, FlightId")] BookFlight bookFlight)
        {
            if (id != bookFlight.BookingFlightId)
            {
                return NotFound();
            }

            var flight = await _db.Flights.FirstOrDefaultAsync(f => f.FlightId == bookFlight.FlightId);

            if (flight != null)
            {
                int currentBookings = await _db.BookingFlights.CountAsync(bf => bf.FlightId == bookFlight.FlightId);

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
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Flights = new SelectList(_db.Flights, "FlightId", "FlightNumber", bookFlight.FlightId);
            return View(bookFlight);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteFlightBooking = await _db.BookingFlights.Include(bf => bf.Flight).FirstOrDefaultAsync(bf => bf.BookingFlightId == id);

            if (deleteFlightBooking == null)
            {
                return NotFound();
            }

            return View(deleteFlightBooking);
        }

        [HttpPost, ActionName("DeleteConfirmed/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteFlightBooking = await _db.BookingFlights.FindAsync(id);

            if (deleteFlightBooking != null)
            {
                _db.BookingFlights.Remove(deleteFlightBooking);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
