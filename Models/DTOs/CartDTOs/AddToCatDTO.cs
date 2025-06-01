using System.ComponentModel.DataAnnotations;

namespace Online_Bookstore.Models.DTOs.CartDTOs
{
	public class AddToCartDTO
	{
		[Required]
		public int BookId { get; set; }

		[Required]
		[Range(1, 100, ErrorMessage = "Quantity must be at least 1.")]
		public int Quantity { get; set; }
	}
}
