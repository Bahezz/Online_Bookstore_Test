using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Bookstore.Models.DTOs.BookDTOs
{
	public class UpdateBookDTO
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(200)]
		public string Title { get; set; }

		[Required]
		[MaxLength(100)]
		public string Author { get; set; }

		[Required]
		[MaxLength(50)]
		public string Genre { get; set; }

		[Required]
		[Column(TypeName = "decimal(10,2)")]
		public decimal Price { get; set; }

		public bool IsAvailable { get; set; }
	}
}
