﻿using System.ComponentModel.DataAnnotations;

namespace Divena_CMS.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
