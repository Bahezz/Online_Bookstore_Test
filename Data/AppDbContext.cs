using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore.Models;
using Online_Bookstore.Models.AuthModel;

namespace Online_Bookstore.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
	
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
		{
			
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<CartItem>()
				.HasOne(c => c.User)
				.WithMany(u => u.CartItems)
				.HasForeignKey(c => c.UserId);

			builder.Entity<CartItem>()
				.HasOne(c => c.Book)
				.WithMany(b => b.CartItems)
				.HasForeignKey(c => c.BookId);

			builder.Entity<OrderItem>()
				.HasOne(oi => oi.Order)
				.WithMany(o => o.Items)
				.HasForeignKey(oi => oi.OrderId);

			builder.Entity<OrderItem>()
				.HasOne(oi => oi.Book)
				.WithMany(b => b.OrderItems)
				.HasForeignKey(oi => oi.BookId);

			builder.Entity<Order>()
				.HasOne(o => o.User)
				.WithMany(u => u.Orders)
				.HasForeignKey(o => o.UserId);
		}
	}

}

