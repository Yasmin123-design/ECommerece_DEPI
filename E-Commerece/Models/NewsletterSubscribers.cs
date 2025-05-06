using System.ComponentModel.DataAnnotations;

namespace E_Commerece.Models
{
	public class NewsletterSubscribers
	{
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
	}
}
