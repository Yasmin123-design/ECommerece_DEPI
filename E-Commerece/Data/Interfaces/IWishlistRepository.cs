using E_Commerece.Models;

namespace E_Commerece.Data.Interfaces
{
    public interface IWishlistRepository
    {
        Wishlist GetWishlistByUserId();
    }
}
