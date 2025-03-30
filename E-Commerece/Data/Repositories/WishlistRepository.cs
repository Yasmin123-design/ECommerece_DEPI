using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerece.Data.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly EcommereceContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        public WishlistRepository(EcommereceContext context)
        {
            this._context = context;
        }
        public Wishlist GetWishlistByUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return null; // أو يمكن إرجاع Wishlist فارغة لتجنب الأخطاء
            }

            var wishlist = _context.Wishlists
                .Include(x => x.Products)
                .FirstOrDefault(x => x.UserId == userId);

            return wishlist; // ✅ يجب إرجاع الـ wishlist
        }

    }
}
