using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerece.Data.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        private readonly EcommereceContext _context;
        public CartItemRepository(EcommereceContext context)
        {
            this._context = context;
        }
        public bool AddCartItem(int productid , ProductItem productItem , int quantity )
        {
            var product = _context.Products.Where(x => x.Id == productid).FirstOrDefault();
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = _context.Carts.Include(c => c.CartItems)
                             .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                // إذا لم يكن للمستخدم سلة تسوق، يتم إنشاء واحدة جديدة
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
            }

            var cartitem = cart.CartItems.Where(x => x.ProductItemId == productItem.Id).FirstOrDefault();

            if(cartitem == null)
            {
                cartitem = new CartItem { ProductItemId = productItem.Id, Quantity = quantity, ProductItem = productItem, CartId = cart.Id };
                cart.CartItems.Add(cartitem);
                this._context.CartItems.Add(cartitem);
                cart.TotalPrice += (product.Price*cartitem.Quantity);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public List<CartItem> GetAllItemsByCartId(int cartid) => this._context.CartItems.Where(x => x.CartId == cartid).ToList();

        public CartItem GetCartItem(int itemid) => _context.CartItems.FirstOrDefault(ci => ci.Id == itemid);

        public void RemoveAllItemsRelatedByCart(int cartid)
        {
            var cartitems = GetAllItemsByCartId(cartid);
            this._context.CartItems.RemoveRange(cartitems);
        }

        public void RemoveCartItem(int id)
        {
            var DeletedCartItem = GetCartItem(id);
            if(DeletedCartItem != null)
            {
                _context.CartItems.Remove(DeletedCartItem);
            }
            
        }
        public List<CartItem> GetCartItems()
        {
            string userId =  _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            return _context.CartItems
                           .Include(ci => ci.ProductItem)
                           .Include(x => x.Cart)
                           .Where(ci => ci.Cart.UserId == userId)
                           .ToList();
        }

    }
}
