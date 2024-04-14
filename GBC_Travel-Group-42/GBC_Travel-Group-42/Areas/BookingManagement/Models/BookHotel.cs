using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GBC_Travel_Group_42.Areas.BookingDetails.Models;

namespace GBC_Travel_Group_42.Areas.BookingManagement.Models
{
    public class BookHotel
    {
        [Key]
        public int BookingHotelId { get; set; }

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

        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Check-In Time")]
        public DateTime CheckInDateTime { get; set; }

        [Required]
        [Display(Name = "Stay Duration")]
        public int ExpectedStayDuration { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Check-Out Time")]
        public DateTime CheckOutDateTime { get; set; }

        [Display(Name = "Total Price")]
        public decimal HotelTotalPrice { get; set; }
    }
}
