using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore.Data;
using Online_Bookstore.Models.DTOs.OrderDTOs;
using System.Security.Claims;

namespace Online_Bookstore.Controllers
{
	[ApiController]
	[Route("api/Order")]
	[Authorize]
	public class OrderController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		public OrderController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<IActionResult> GetOrders()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			try
			{
				var orders = await _context.Orders
					.Where(o => o.UserId == userId)
					.Include(o => o.Items)
						.ThenInclude(i => i.Book)
					.OrderByDescending(o => o.CreatedAt)
					.ToListAsync();

				var dto = _mapper.Map<List<OrderDTO>>(orders);
				return Ok(dto);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		
		[HttpGet("{orderId:int}")]
		public async Task<IActionResult> GetOrder(int orderId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			try
			{
				var order = await _context.Orders
					.Where(o => o.UserId == userId && o.Id == orderId)
					.Include(o => o.Items)
						.ThenInclude(i => i.Book)
					.FirstOrDefaultAsync();

				if (order == null)
					return NotFound("Order not found.");

				var dto = _mapper.Map<OrderDTO>(order);
				return Ok(dto);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
