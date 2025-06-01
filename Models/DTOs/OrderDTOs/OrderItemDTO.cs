namespace Online_Bookstore.Models.DTOs.OrderDTOs
{
	public class OrderItemDTO
	{
		public int BookId { get; set; }
		public string Title { get; set; } // Book Title
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
