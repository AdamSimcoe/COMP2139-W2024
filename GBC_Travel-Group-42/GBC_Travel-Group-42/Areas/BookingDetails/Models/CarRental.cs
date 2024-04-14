using System.ComponentModel.DataAnnotations;

namespace GBC_Travel_Group_42.Areas.BookingDetails.Models
{
    public class CarRental
    {
        [Key]
        public int CarRentalId { get; set; }

        [Required]
        [Display(Name = "Car Make")]
        public string CarRentalMake { get; set; }

        [Required]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Maximum Occupancy")]
        public int MaximumOccupancy { get; set; }
    }
}
