using E_Commerece.Data.Interfaces;
using E_Commerece.Models;

namespace E_Commerece.Data.Repositories
{
	public class NewsletterSubscribersRepository : INewsletterSubscribersRepository
	{
		private readonly EcommereceContext _context;
		public NewsletterSubscribersRepository(EcommereceContext context)
		{
			this._context = context;
		}
		public void CreateNew(string email)
		{
			var newSletter = new NewsletterSubscribers()
			{
				Email = email
			};
			this._context.NewsletterSubscribers.Add(newSletter);
		}

		public bool EmailExists(string email)
		{
			var checkedEmail = this._context.NewsletterSubscribers.Where(x => x.Email == email);
			return checkedEmail.Any();
		}
	}
}
