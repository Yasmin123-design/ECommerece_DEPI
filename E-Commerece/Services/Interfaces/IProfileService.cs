using E_Commerece.Models;
using E_Commerece.ViewModels;

namespace E_Commerece.Services.Interfaces
{
    public interface IProfileService
    {
        User GetUserById();
        void EditUserInfo(EditUserInfoVM user);
        void SaveChange();
    }
}
