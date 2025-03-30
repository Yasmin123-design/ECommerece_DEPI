using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface IProfileRepository
    {
        User GetUserById();
        void EditUserInfo(User user);
    }
}
