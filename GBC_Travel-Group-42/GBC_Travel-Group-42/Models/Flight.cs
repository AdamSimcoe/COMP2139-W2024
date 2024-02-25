using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_42.Models
{
	public class Flight
	{
		[Key]
		public  int FlightId { get; set; }

		[Required]
		[Display(Name = "Flight Number")]
		public string FlightNumber { get; set; }

		[Required]
		public string Origin { get; set; }

		[Required]
		public string Destination { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Display(Name = "Departure Time")]
		public DateTime DepartureDateTime { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Display(Name = "Arrival Time")]
		public DateTime ArrivalDateTime { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Range(1, 5)]
		public int Rating { get; set; }

		[Required]
		[Display (Name = "Maximum Occupancy")]
		public int MaximumOccupancy { get; set; }
	}		
}
