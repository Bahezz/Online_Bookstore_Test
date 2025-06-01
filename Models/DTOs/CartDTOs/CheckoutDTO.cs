using System.ComponentModel.DataAnnotations;

namespace Online_Bookstore.Models.DTOs.CartDTOs
{
	public class CheckoutDTO
	{
		[Required]
		[StringLength(250)]
		public string Address { get; set; }
	}
}
