namespace E_Commerece.Data.Interfaces
{
	public interface INewsletterSubscribersRepository
	{
		void CreateNew(string email);
		bool EmailExists(string email);
	}
}
