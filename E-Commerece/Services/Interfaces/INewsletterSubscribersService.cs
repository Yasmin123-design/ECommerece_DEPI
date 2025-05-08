namespace E_Commerece.Services.Interfaces
{
	public interface INewsletterSubscribersService
	{
		void CreateNew(string email);
		bool EmailExists(string email);
        void SaveChange();
    }
}
