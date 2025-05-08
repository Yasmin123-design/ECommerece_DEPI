using E_Commerece.Data.Interfaces;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
	public class NewsletterSubscribersService : INewsletterSubscribersService
	{
		private readonly IUnitOfWork _unitOfWork;
		public NewsletterSubscribersService(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;
		}
		public void CreateNew(string email)
		{
			this._unitOfWork.NewsletterSubscribers.CreateNew(email);
		}

		public bool EmailExists(string email) => this._unitOfWork.NewsletterSubscribers.EmailExists(email);

        public void SaveChange()
        {
			this._unitOfWork.Save();
		}
    }
}
