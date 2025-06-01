using System.ComponentModel.DataAnnotations;

namespace Online_Bookstore.Models.DTOs.AuthDTOs
{
	public class LoginModel
	{
		[Required]
		[EmailAddress]

		public string Email { get; set; }
		[Required]

		[DataType(DataType.Password)]

		public string Password { get; set; }
	}
}
