using E_Commerece.Models;
using E_Commerece.ViewModels;

namespace E_Commerece.Data.Interfaces
{
    public interface IProfileRepository
    {
        User GetUserById();
        void EditUserInfo(EditUserInfoVM user);
    }
}
