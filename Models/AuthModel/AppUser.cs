using Microsoft.AspNetCore.Identity;

namespace Online_Bookstore.Models.AuthModel
{
	public class AppUser : IdentityUser
	{
		public string FullName { get; set; }

		//public string Address { get; set; }

		public ICollection<Order> Orders { get; set; }
		public ICollection<CartItem> CartItems { get; set; }

	}
}
