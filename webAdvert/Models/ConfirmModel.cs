using System;
using System.ComponentModel.DataAnnotations;

namespace webAdvert.Models
{
	public class ConfirmModel
	{
		[Required]
		public string Email { get; set; }
        [Required]
        public string Code { get; set; }
	}
}

