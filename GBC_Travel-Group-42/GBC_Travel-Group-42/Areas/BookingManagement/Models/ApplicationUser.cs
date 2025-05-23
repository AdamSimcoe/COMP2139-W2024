﻿using Microsoft.AspNetCore.Identity;

namespace GBC_Travel_Group_42.Areas.BookingManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UsernameChangeLimit { get; set; } = 10;

        public byte[]? ProfilePicture { get; set; }
    }
}
