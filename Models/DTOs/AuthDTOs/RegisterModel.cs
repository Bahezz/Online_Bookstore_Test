using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Bookstore.Models.DTOs.AuthDTOs
{
	public class RegisterModel
	{
		[Required]
		public string FullName { get; set; }

		[Required]
		[EmailAddress]
	
		public string Email { get; set; }
		[Required]
		
		[DataType(DataType.Password)]
		
		public string Password { get; set; }

		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
