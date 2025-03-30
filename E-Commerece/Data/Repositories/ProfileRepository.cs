using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace E_Commerece.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly EcommereceContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        public ProfileRepository(EcommereceContext context)
        {
            this._context = context;
        }

        public void EditUserInfo(User user)
        {
            var olduser = GetUserById();
            olduser.UserName = user.UserName;
            olduser.Email = user.Email;
            olduser.Address = user.Address;
        }

        public User GetUserById()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (user == null) throw new Exception("user not found");
            return user;
        }
    }
}
