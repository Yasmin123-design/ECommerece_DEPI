using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerece.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommereceContext _context;
        public UserRepository(EcommereceContext context)
        {
            this._context = context;
        }
        public int GetUserCount()
        {
            var totalUsers = (from user in _context.Users
                              join userRole in _context.UserRoles on user.Id equals userRole.UserId
                              join role in _context.Roles on userRole.RoleId equals role.Id
                              where role.Name == "User"
                              select user).Count();
            return totalUsers;

        }
    }
}
