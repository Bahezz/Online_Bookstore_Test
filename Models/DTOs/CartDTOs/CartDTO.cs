using Online_Bookstore.Models.DTOs.BookDTOs;
using System.ComponentModel.DataAnnotations;

namespace Online_Bookstore.Models.DTOs.CartDTOs
{
	public class CartDTO
	{
		public int Id { get; set; }

		[Required]
		
		public string UserId { get; set; }
		

		[Required]
		
		public int BookId { get; set; }

		public BookDTO Book { get; set; }

		[Required]
		[Range(1, 100)]
		public int Quantity { get; set; }
	}
}
