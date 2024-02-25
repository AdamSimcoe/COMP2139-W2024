using Microsoft.EntityFrameworkCore;
using GBC_Travel_Group_42.Models;

namespace GBC_Travel_Group_42.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Flight> Flights { get; set; }

		public DbSet<Hotel> Hotels { get; set; }

		public DbSet<CarRental> CarRentals { get; set; }

		public DbSet<BookFlight> BookingFlights { get; set; }
		
		public DbSet<BookHotel> BookingHotels { get; set; }

		public DbSet<BookCarRental> BookingCarRentals { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Flight>().HasData(
			   new Flight { FlightId = 1, FlightNumber = "KH734", Origin = "Toronto, CA", Destination = "Tokyo, JP", DepartureDateTime = new DateTime(2024, 3, 1, 11, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 1, 22, 30, 0), Price = 1349.99m, Rating = 5, MaximumOccupancy = 2 },
			   new Flight { FlightId = 2, FlightNumber = "KH767", Origin = "Montreal, CA", Destination = "Sydney, AU", DepartureDateTime = new DateTime(2024, 3, 1, 9, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 2, 1, 30, 0), Price = 1299.99m, Rating = 4, MaximumOccupancy = 2 },
			   new Flight { FlightId = 3, FlightNumber = "KF647", Origin = "London, UK", Destination = "Los Angeles, US", DepartureDateTime = new DateTime(2024, 3, 2, 13, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 2, 17, 0, 0), Price = 899.99m, Rating = 3, MaximumOccupancy = 2 },
			   new Flight { FlightId = 4, FlightNumber = "KF867", Origin = "New York City, US", Destination = "Paris, FR", DepartureDateTime = new DateTime(2024, 3, 2, 7, 0, 0), ArrivalDateTime = new DateTime(2024, 3, 2, 10, 30, 0), Price = 1099.99m, Rating = 5, MaximumOccupancy = 2 },
			   new Flight { FlightId = 5, FlightNumber = "KR765", Origin = "Athens, GR", Destination = "Hong Kong, CN", DepartureDateTime = new DateTime(2024, 3, 3, 8, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 3, 20, 30, 0), Price = 799.99m, Rating = 2, MaximumOccupancy = 2 },
			   new Flight { FlightId = 6, FlightNumber = "KR456", Origin = "Tokyo, JP", Destination = "New York City, US", DepartureDateTime = new DateTime(2024, 3, 3, 6, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 3, 14, 30, 0), Price = 859.99m, Rating = 2, MaximumOccupancy = 2 },
			   new Flight { FlightId = 7, FlightNumber = "KG699", Origin = "Hong Kong, CN", Destination = "London, UK", DepartureDateTime = new DateTime(2024, 3, 4, 12, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 5, 3, 30, 0), Price = 749.99m, Rating = 1, MaximumOccupancy = 2 },
			   new Flight { FlightId = 8, FlightNumber = "KG921", Origin = "Los Angeles, US", Destination = "Montreal, CA", DepartureDateTime = new DateTime(2024, 3, 4, 10, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 4, 15, 30, 0), Price = 699.99m, Rating = 1, MaximumOccupancy = 2 },
			   new Flight { FlightId = 9, FlightNumber = "KK432", Origin = "Paris, FR", Destination = "Toronto, CA", DepartureDateTime = new DateTime(2024, 3, 5, 13, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 5, 18, 30, 0), Price = 1349.99m, Rating = 3, MaximumOccupancy = 2 },
			   new Flight { FlightId = 10, FlightNumber = "KK420", Origin = "Sydney, AU", Destination = "Athens, GR", DepartureDateTime = new DateTime(2024, 3, 5, 11, 30, 0), ArrivalDateTime = new DateTime(2024, 3, 6, 1, 30, 0), Price = 1349.99m, Rating = 4, MaximumOccupancy = 2 }
			);

			modelBuilder.Entity<Hotel>().HasData(
				new Hotel { HotelId = 1, HotelName = "Arion Athens Hotel", HotelLocation = "Ag. Dimitriou 18, Athina 105 54, Greece", PricePerNight = 399.99m, Rating = 4, MaximumOccupancy = 2 },
				new Hotel { HotelId = 2, HotelName = "Hotel Le Grand Mazarin", HotelLocation = "7 Rue de la Verrerie, 75004 Paris, France", PricePerNight = 499.99m, Rating = 5, MaximumOccupancy = 2 },
				new Hotel { HotelId = 3, HotelName = "Hotel New Otani Tokyo", HotelLocation = "4-1 Kioicho, Chiyoda City, Tokyo 102-8578, Japan", PricePerNight = 549.99m, Rating = 5, MaximumOccupancy = 2 },
				new Hotel { HotelId = 4, HotelName = "Sheraton Grand Sydney Hyde Park", HotelLocation = "161 Elizabeth St, Sydney NSW 2000, Australia", PricePerNight = 349.99m, Rating = 4, MaximumOccupancy = 2 },
				new Hotel { HotelId = 5, HotelName = "Hilton New York Times Square", HotelLocation = "234 W 42nd St, New York, NY 10036, United States", PricePerNight = 449.99m, Rating = 3, MaximumOccupancy = 2 },
				new Hotel { HotelId = 6, HotelName = "Ramada Hong Kong Grand", HotelLocation = "23 Austin Ave, Tsim Sha Tsui, Hong Kong, China", PricePerNight = 399.99m, Rating = 3, MaximumOccupancy = 2 },
				new Hotel { HotelId = 7, HotelName = "Corus Hotel Hyde Park", HotelLocation = "1 Lancaster Gate, London W2 3LG, United Kingdom", PricePerNight = 299.99m, Rating = 2, MaximumOccupancy = 2 },
				new Hotel { HotelId = 8, HotelName = "Gite du Survenant Montreal", HotelLocation = "2645 Av. Desjardins, Montréal, QC H1V 2H8, Canada", PricePerNight = 249.99m, Rating = 2, MaximumOccupancy = 2 },
				new Hotel { HotelId = 9, HotelName = "One King West Hotel & Residence", HotelLocation = "1 King St W, Toronto, ON M5H 1A1, Canada", PricePerNight = 399.99m, Rating = 1, MaximumOccupancy = 2 },
				new Hotel { HotelId = 10, HotelName = "Four Points by Sheraton LA International Airport", HotelLocation = "9750 Airport Blvd, Los Angeles, CA 90045, United States", PricePerNight = 299.99m, Rating = 1, MaximumOccupancy = 2 }
			);

			modelBuilder.Entity<CarRental>().HasData(
				new CarRental { CarRentalId = 1, CarRentalMake = "2024 Nissan Rogue", PricePerDay = 149.99m, Rating = 5, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 2, CarRentalMake = "2019 Chevrolet Malibu", PricePerDay = 199.99m, Rating = 5, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 3, CarRentalMake = "2024 Toyota Sienna", PricePerDay = 99.99m, Rating = 4, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 4, CarRentalMake = "2024 Mitsubishi Mirage", PricePerDay = 79.99m, Rating = 4, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 5, CarRentalMake = "2019 Toyota Corolla", PricePerDay = 49.99m, Rating = 3, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 6, CarRentalMake = "2024 Ford Transit Van", PricePerDay = 59.99m, Rating = 3, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 7, CarRentalMake = "2021 Nissan NV200", PricePerDay = 69.99m, Rating = 2, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 8, CarRentalMake = "2024 Ford Edge", PricePerDay = 129.99m, Rating = 2, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 9, CarRentalMake = "2024 Audi Q3", PricePerDay = 119.99m, Rating = 1, MaximumOccupancy = 2 },
				new CarRental { CarRentalId = 10, CarRentalMake = "2023 Chevrolet Express", PricePerDay = 89.99m, Rating = 1, MaximumOccupancy = 2 }
			);

			modelBuilder.Entity<BookFlight>().HasData(
				new BookFlight { BookingFlightId = 1, GuestFirstName = "John", GuestLastName = "Doe", GuestEmail = "fakeemail@yes.com", Accommodations = "Need extra luggage space", FlightId = 1 }
			);

			modelBuilder.Entity<BookHotel>().HasData(
				 new BookHotel { BookingHotelId = 1, GuestFirstName = "Jane", GuestLastName = "Doe", GuestEmail = "fakeemail2@yes.com", Accommodations = "Need extra towels", HotelId = 1, CheckInDateTime = new DateTime(2024, 2, 22, 13, 30, 0), ExpectedStayDuration = 3, CheckOutDateTime = new DateTime(2024, 2, 25, 13, 30, 0), HotelTotalPrice = 1199.97m }
            );

			modelBuilder.Entity<BookCarRental>().HasData(
				 new BookCarRental { BookingCarRentalId = 1, GuestFirstName = "Adam", GuestLastName = "Simcoe", GuestEmail = "fakeemail3@yes.com", Accommodations = "extra seating", CarRentalId = 1, RentalPickupDateTime = new DateTime(2024, 2, 21, 10, 30, 0), RentalDuration = 3, RentalReturnDateTime = new DateTime(2024, 2, 24, 10, 30, 0), RentalTotalPrice = 449.97m }
            );

			base.OnModelCreating(modelBuilder);
		}
	}
}
