using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore.Data;
using Online_Bookstore.Models;
using Online_Bookstore.Models.DTOs.CartDTOs;
using System.Security.Claims;

namespace Online_Bookstore.Controllers
{
	[Route("api/Cart")]
	[ApiController]
	[Authorize]
	public class CartController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public CartController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetCart()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var cartItems = await _context.CartItems
					.Include(c => c.Book)
					.Where(c => c.UserId == userId)
					.ToListAsync();
				var dto = _mapper.Map<List<CartDTO>>(cartItems);
				return Ok(dto);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var book = await _context.Books.FindAsync(model.BookId);

				if (book == null)
					return NotFound("Book not found.");

				if (!book.IsAvailable)
					return BadRequest("This book is currently not available.");
				var existingItem = await _context.CartItems
					.FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == model.BookId);

				if (existingItem != null)
				{
					existingItem.Quantity += model.Quantity;
				}
				else
				{
					var newItem = new CartItem
					{
						UserId = userId,
						BookId = model.BookId,
						Quantity = model.Quantity
					};
					_context.CartItems.Add(newItem);
				}

				await _context.SaveChangesAsync();
				return StatusCode(201, "Item added to cart.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("remove/{bookId:int}")]
		public async Task<IActionResult> RemoveFromCart(int bookId)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var item = await _context.CartItems
					.FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == bookId);

				if (item == null)
					return NotFound("Item not found in cart.");

				_context.CartItems.Remove(item);
				await _context.SaveChangesAsync();

				return Ok("Item removed from cart.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("checkout")]
		public async Task<IActionResult> Checkout([FromBody] CheckoutDTO model)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var cartItems = await _context.CartItems
					.Include(c => c.Book)
					.Where(c => c.UserId == userId)
					.ToListAsync();

				if (!cartItems.Any())
					return BadRequest("Cart is empty.");
				foreach (var item in cartItems)
				{
					if (!item.Book.IsAvailable)
						return BadRequest($"Book '{item.Book.Title}' is not available.");
				}
				decimal total = cartItems.Sum(c => c.Book.Price * c.Quantity);

				var order = new Order
				{
					UserId = userId,
					CreatedAt = DateTime.UtcNow,
					TotalAmount = total,
					Items = cartItems.Select(c => new OrderItem
					{
						BookId = c.BookId,
						Quantity = c.Quantity,
						UnitPrice = c.Book.Price
					}).ToList(),
					ShippingAddress = model.Address
				};

				_context.Orders.Add(order);
				// Update book availability
				foreach (var item in cartItems)
				{
					item.Book.IsAvailable = false;
				}
				_context.CartItems.RemoveRange(cartItems);
				await _context.SaveChangesAsync();

				return StatusCode(201, "Order placed successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}