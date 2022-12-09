using System;
using System.ComponentModel.DataAnnotations;

namespace webAdvert.Models
{
	public class ResetModel
	{
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string NewPassword { get; set; }

    }
}

