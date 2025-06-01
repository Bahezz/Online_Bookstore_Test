using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Online_Bookstore.Models.AuthModel;
using Online_Bookstore.Models.DTOs.AuthDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Online_Bookstore.Controllers
{
	[Route("api/Auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManger;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IConfiguration _config;

		public AuthController(UserManager<AppUser> userManger, SignInManager<AppUser> signInManager, IConfiguration config)
		{
			_userManger = userManger;
			_signInManager = signInManager;
			_config = config;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			try
			{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if(model.Password != model.ConfirmPassword) return BadRequest("The Password and ComfirmPassword Does not Match");

			var user = new AppUser { 
				UserName = model.Email,
				Email = model.Email,
				FullName = model.FullName};
			var result = await _userManger.CreateAsync(user, model.Password);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			//	return CreatedAtAction
				return Ok("User registered successfully.");
			}
			catch (Exception ex) 
			{
				return StatusCode(500, ex.Message);
			}


		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if(model == null) return BadRequest("Invaled data set!");
			try
			{
				var user = await _userManger.FindByEmailAsync(model.Email);
				if (user == null || !await _userManger.CheckPasswordAsync(user, model.Password)) return Unauthorized("Invalid Password or Email.");

				var authClaims = new List<Claim>
				{
					// it should be more then this two claims but for this test project that is enugh!
					new Claim(ClaimTypes.NameIdentifier, user.Id),
					new Claim(ClaimTypes.Email,user.Email)
				};
				var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new NullReferenceException("there is no Key to create the toke!"));
				var authSigninKey = new SymmetricSecurityKey(key);
				var token = new JwtSecurityToken(
					issuer: _config["Jwt:Issuer"],
					audience: _config["Jwt:Audience"],
					expires: DateTime.UtcNow.AddHours(2),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
				);
				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo,
					email = user.Email
				});

			}
			catch (Exception ex) {

				return StatusCode(500, ex.Message);
			}
		}

	}
}
