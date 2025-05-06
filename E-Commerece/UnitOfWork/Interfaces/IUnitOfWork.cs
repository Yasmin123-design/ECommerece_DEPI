using E_Commerece.Data.Interfaces;

namespace E_Commerece.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductItemRepository ProductItems { get; }
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICartItemRepository CartItems { get; }
        ICartRepository Carts { get; }
        IOrderRepository Orders { get; }
        IWishlistRepository Wishlist { get; }
        IProfileRepository Profile { get; }
        IVariationRepository Variations { get; }

        IVariationOptionsRepository VariationOptions { get; }
        IUserRepository Users { get; }
        void Save();
    }
}
