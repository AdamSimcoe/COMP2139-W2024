using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GBC_Travel_Group_42.Areas.BookingDetails.Models;

namespace GBC_Travel_Group_42.Areas.BookingManagement.Models
{
    public class BookCarRental
    {
        [Key]
        public int BookingCarRentalId { get; set; }

		public string? UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual ApplicationUser? User { get; set; }

		[Required]
        [Display(Name = "First Name")]
        public string GuestFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string GuestLastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string GuestEmail { get; set; }
        public string? Accommodations { get; set; }

        public int CarRentalId { get; set; }

        public CarRental? CarRental { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Rental Pickup Time")]
        public DateTime RentalPickupDateTime { get; set; }

        [Required]
        [Display(Name = "Rental Duration")]
        public int RentalDuration { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Rental Return Time")]
        public DateTime RentalReturnDateTime { get; set; }

        [Display(Name = "Total Price")]
        public decimal RentalTotalPrice { get; set; }
    }
}
