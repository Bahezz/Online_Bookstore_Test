using AutoMapper;
using Online_Bookstore.Models;
using Online_Bookstore.Models.DTOs.BookDTOs;
using Online_Bookstore.Models.DTOs.CartDTOs;
using Online_Bookstore.Models.DTOs.OrderDTOs;

namespace Online_Bookstore
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			#region Book Mapping

			CreateMap<Book, BookDTO>().ReverseMap();
			CreateMap<Book, CreateBookDTO>().ReverseMap();
			CreateMap<Book, UpdateBookDTO>().ReverseMap();

			#endregion
			#region Cart Item Mapping

			CreateMap<CartItem, CartDTO>()
			.ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

			CreateMap<CartDTO, CartItem>()
				.ForMember(dest => dest.Book, opt => opt.Ignore()) // Book is a navigation property, should not be mapped directly
				.ForMember(dest => dest.User, opt => opt.Ignore()); // Same for User

			CreateMap<CartItem, AddToCartDTO>().ReverseMap();
			#endregion
			#region Order Mapping
			CreateMap<OrderItem, OrderItemDTO>()
		   .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title));

			CreateMap<Order, OrderDTO>();
			#endregion
		}
	}
}
