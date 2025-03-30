using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerece.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly EcommereceContext _context;
        public CartRepository(EcommereceContext context)
        {
            this._context = context;
        }

        public void ClearCart(string userid)
        {
            var cart = _context.Carts.Where(x => x.UserId == userid).FirstOrDefault();
            cart.TotalPrice = 0;

            var cartItems = _context.CartItems.Include(x => x.Cart).Where(x => x.Cart.UserId == userid).ToList();
            _context.CartItems.RemoveRange(cartItems);
        }

        public Cart GetCartById(int id) => this._context.Carts.Where(x => x.Id == id).FirstOrDefault();

        public Cart GetCartByUserId(string userId) => this._context.Carts
            .Include(c => c.CartItems)
        .ThenInclude(ci => ci.ProductItem)
            .ThenInclude(pi => pi.Product) // تضمين المنتج داخل ProductItem
    .Include(c => c.CartItems)
        .ThenInclude(ci => ci.ProductItem)
            .ThenInclude(pi => pi.VariationOptions) // تضمين قائمة خيارات التباين داخل ProductItem
            .ThenInclude(x => x.Variation)
    .Where(c => c.UserId == userId)
    .FirstOrDefault();
    }
}
