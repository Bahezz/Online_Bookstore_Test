using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore.Data;
using Online_Bookstore.Models;
using Online_Bookstore.Models.DTOs.BookDTOs;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Online_Bookstore.Controllers
{
	[Route("api/Book")]
	[ApiController]

	public class BookController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public BookController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get() {

			try
			{
				var Result = await _context.Books.ToListAsync();

				var dto = _mapper.Map<List<BookDTO>>(Result);

				return Ok(dto);
			}
			catch (Exception ex) { 
			
				return StatusCode(500, ex.Message);
			
			}
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetBook(int id)
		{
			if (id == 0) return BadRequest("Invalid data set");
			try
			{
				var result = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

				

				var dto = _mapper.Map<BookDTO>(result);

				return Ok(dto);
				
				

			}
			catch (Exception ex)
			{

				return StatusCode(500, ex.Message);

			}
		}
		
		[HttpGet("search")]
		public async Task<IActionResult> SearchBooks([FromQuery] string? query, [FromQuery] string? genre)
		{
			try
			{
				var booksQuery = _context.Books.AsQueryable();

				if (!string.IsNullOrWhiteSpace(query))
				{
					booksQuery = booksQuery.Where(b =>
						b.Title.Contains(query) ||
						b.Author.Contains(query) ||
						b.Genre.Contains(query));
				}

				if (!string.IsNullOrWhiteSpace(genre))
				{
					booksQuery = booksQuery.Where(b => b.Genre.ToLower() == genre.ToLower());
				}

				var books = await booksQuery.ToListAsync();
				var bookDTOs = _mapper.Map<List<BookDTO>>(books);

				return Ok(bookDTOs);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateBook(CreateBookDTO dto)
		{
			if (dto == null) return BadRequest("Invalid data set");
			try
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var bookToAdd = _mapper.Map<Book>(dto);
			await _context.Books.AddAsync(bookToAdd);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetBook), new { id = bookToAdd.Id }, dto);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);

			}
		

		}
		[Authorize]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id, UpdateBookDTO dto)
		{
			if(id == 0 || dto == null) BadRequest("Invalid data set");
			if (id != dto.Id) return BadRequest("Book ID mismatch.");
			try
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var bookToUpdate = _mapper.Map<Book>(dto);

				_context.Books.Update(bookToUpdate);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				if (!_context.Books.Any(b => b.Id == id))
					return NotFound();

				return StatusCode(409, "there is a Confilect plesae try again!");
			}
			catch (Exception ex) 
			{
				return StatusCode(500, ex.Message);
			}
			

			

			
		}

		[Authorize]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			if (id == 0 ) BadRequest("Invalid data set");
			try
			{
				var book = await _context.Books.FindAsync(id);
				if (book == null)
					return NotFound();

				_context.Books.Remove(book);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
			
		}


	}
}
