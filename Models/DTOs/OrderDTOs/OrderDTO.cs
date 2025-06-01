namespace Online_Bookstore.Models.DTOs.OrderDTOs
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public decimal TotalAmount { get; set; }
		public string ShippingAddress { get; set; }
		public List<OrderItemDTO> Items { get; set; }
	}
}
