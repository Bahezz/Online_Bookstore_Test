using Online_Bookstore.Models.AuthModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Bookstore.Models
{
	public class Order
	{
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public string UserId { get; set; }
		public AppUser User { get; set; }

		public List<OrderItem> Items { get; set; }

		[Required]
		[Column(TypeName = "decimal(10,2)")]
		public decimal TotalAmount { get; set; }

		[Required]
		[MaxLength(500)]
		public string ShippingAddress { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
