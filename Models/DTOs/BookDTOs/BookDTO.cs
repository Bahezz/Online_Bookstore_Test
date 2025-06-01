using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Bookstore.Models.DTOs.BookDTOs
{
	public class BookDTO
	{

		public int Id { get; set; }
		public string Title { get; set; }

		
		public string Author { get; set; }

		
		public string Genre { get; set; }

	
		[Column(TypeName = "decimal(10,2)")]
		public decimal Price { get; set; }

		public bool IsAvailable { get; set; }

		
	}
}
