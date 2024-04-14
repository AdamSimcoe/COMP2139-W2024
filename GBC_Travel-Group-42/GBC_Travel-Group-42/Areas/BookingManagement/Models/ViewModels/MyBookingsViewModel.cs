namespace GBC_Travel_Group_42.Areas.BookingManagement.Models.ViewModels
{
	public class MyBookingsViewModel
	{
		public List<BookFlight> FlightBookings { get; set; }

		public List<BookHotel> HotelBookings { get; set; }

		public List<BookCarRental> CarRentalBookings { get; set; }
	}
}
