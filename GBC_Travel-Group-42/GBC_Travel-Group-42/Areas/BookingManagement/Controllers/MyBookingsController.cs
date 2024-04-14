using GBC_Travel_Group_42.Areas.BookingManagement.Models.ViewModels;
using GBC_Travel_Group_42.Areas.BookingManagement.Models;
using GBC_Travel_Group_42.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_42.Areas.BookingManagement.Controllers
{
	[Area("BookingManagement")]
	[Route("[area]/[controller]/[action]")]
	public class MyBookingsController : Controller
	{
		private readonly AppDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public MyBookingsController(AppDbContext db, UserManager<ApplicationUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var userId = _userManager.GetUserId(User);
			var flightBookings = await _db.BookingFlights.Include(bf => bf.Flight).Where(bf => bf.UserId == userId).ToListAsync();
			var hotelBookings = await _db.BookingHotels.Include(bh => bh.Hotel).Where(bh => bh.UserId == userId).ToListAsync();
			var carRentalBookings = await _db.BookingCarRentals.Include(bc => bc.CarRental).Where(bc => bc.UserId == userId).ToListAsync();

			var viewModel = new MyBookingsViewModel
			{
				FlightBookings = flightBookings,
				HotelBookings = hotelBookings,
				CarRentalBookings = carRentalBookings
			};

			return View(viewModel);
		}
	}
}
