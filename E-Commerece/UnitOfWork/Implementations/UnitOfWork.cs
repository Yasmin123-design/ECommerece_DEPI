using E_Commerece.Data.Interfaces;
using E_Commerece.Data.Repositories;
using E_Commerece.Migrations;
using E_Commerece.Models;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommereceContext _context;
        public IProductRepository Products { get; }

        public ICategoryRepository Categories { get; }

        public IProductItemRepository ProductItems { get; }

        public ICartItemRepository CartItems { get; }

        public ICartRepository Carts { get; }

		public IOrderRepository Orders { get; }

        public IWishlistRepository Wishlist { get; }

        public IProfileRepository Profile { get; }

        public IVariationRepository Variations { get; }

        public IVariationOptionsRepository VariationOptions { get; }

        public UnitOfWork(EcommereceContext context)
        {
            this._context = context;
            Products = new ProductRepository(_context);
            ProductItems = new ProductItemRepository(_context);
            Categories = new CategoryRepository(_context);
            CartItems = new CartItemRepository(_context);
            Carts = new CartRepository(_context);
            Orders = new OrderRepository(_context);
            Wishlist = new WishlistRepository(_context);
            Profile = new ProfileRepository(_context);
            Variations = new VariationRepository(_context);
            VariationOptions = new VariationOptionsRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
