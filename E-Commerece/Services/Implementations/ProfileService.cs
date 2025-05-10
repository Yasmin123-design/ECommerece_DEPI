using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;
using E_Commerece.ViewModels;

namespace E_Commerece.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfileService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void EditUserInfo(EditUserInfoVM user)
        {
            this._unitOfWork.Profile.EditUserInfo(user);
        }

        public User GetUserById() => this._unitOfWork.Profile.GetUserById();

        public void SaveChange()
        {
            this._unitOfWork.Save();
        }
    }
}
