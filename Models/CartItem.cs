using Online_Bookstore.Models.AuthModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Bookstore.Models
{
	public class CartItem
	{
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public string UserId { get; set; }
		public AppUser User { get; set; }

		[Required]
		[ForeignKey(nameof(Book))]
		public int BookId { get; set; }
		public Book Book { get; set; }

		[Required]
		[Range(1, 100)]
		public int Quantity { get; set; }
	}

}
