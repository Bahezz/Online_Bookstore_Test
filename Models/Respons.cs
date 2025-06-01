using System.Net;

namespace Online_Bookstore.Models
{
	public class Respons
	{
		public Respons()
		{
			ErrorMessages = new List<string>();
		}
		public HttpStatusCode statusCode { get; set; }

		public List<string> ErrorMessages { get; set; }

		public object Data { get; set; }
	}
}
