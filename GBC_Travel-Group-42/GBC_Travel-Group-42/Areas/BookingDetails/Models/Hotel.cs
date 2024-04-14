using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_42.Areas.BookingDetails.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }

        [Required]
        [Display(Name = "Hotel Location")]
        public string HotelLocation { get; set; }

        [Required]
        [Display(Name = "Price Per Night")]
        public decimal PricePerNight { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Maximum Occupancy")]
        public int MaximumOccupancy { get; set; }

    }
}
