using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GBC_Travel_Group_42.Areas.BookingDetails.Models;


namespace GBC_Travel_Group_42.Areas.BookingManagement.Models
{
    public class BookFlight
    {
        [Key]
        public int BookingFlightId { get; set; }

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

        public int FlightId { get; set; }

        public Flight? Flight { get; set; }
    }
}
