using System;
using System.ComponentModel.DataAnnotations;

namespace webAdvert.Models
{
    public class SignupModel
    {
       [Required]
       [EmailAddress]
       public string Email { set; get; }

        [Required]
        public string Password { set; get; }

        [Required]
        public string ConfirmPassword { set; get; }

    }
}

