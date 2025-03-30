using E_Commerece.Models;

namespace E_Commerece.Services.Interfaces
{
    public interface IProfileService
    {
        User GetUserById();
        void EditUserInfo(User user);
        void SaveChange();
    }
}
