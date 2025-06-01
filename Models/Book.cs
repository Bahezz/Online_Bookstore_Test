using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Bookstore.Models
{
	public class Book
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

		public ICollection<OrderItem> OrderItems { get; set; }
		public ICollection<CartItem> CartItems { get; set; }
	}
}
