using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Bookstore.Models
{
	public class OrderItem
	{
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Order))]
		public int OrderId { get; set; }
		public Order Order { get; set; }

		[Required]
		[ForeignKey(nameof(Book))]
		public int BookId { get; set; }
		public Book Book { get; set; }

		[Required]
		[Range(1, 100)]
		public int Quantity { get; set; }

		[Column(TypeName = "decimal(10,2)")]
		public decimal UnitPrice { get; set; }
	}
}
