using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Online_Bookstore;
using Online_Bookstore.Data;
using Online_Bookstore.Models.AuthModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connection = builder.Configuration.GetConnectionString("LocalConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connection);
});
//Auth 
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
});
//Auto Mapper 
builder.Services.AddAutoMapper(typeof(MappingConfig));


//JWT Berrear Token
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new NullReferenceException("there Is No Sercrt To Create the Key!"));
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = false; // this is test It Should be True
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = false,
			ValidateAudience = false,
			ClockSkew = TimeSpan.Zero
		};
	});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(op =>
{
	op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization Header using Bearer Scheme.\r\n\r\n" +
		"Enere Bearer [space] and your token in this input text below.\r\n\r\n " +
		"Exampel: \" Bearer 121fsdhf....\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Scheme = "Bearer"
	});
	op.AddSecurityRequirement(new OpenApiSecurityRequirement
	{

		{
			new OpenApiSecurityScheme
	   {
		   Reference= new OpenApiReference
		   {
			   Type= ReferenceType.SecurityScheme,
			   Id="Bearer"
		   },
		   Scheme="oauth2",
		   Name="Bearer",
		   In=ParameterLocation.Header
		},
	   new List<string>()
		}


	});
});


var app = builder.Build();
app.UseHttpsRedirection();

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
