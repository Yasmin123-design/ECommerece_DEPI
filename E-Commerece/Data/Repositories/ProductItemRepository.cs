using E_Commerece.Data.Interfaces;
using E_Commerece.Models;

namespace E_Commerece.Data.Repositories
{
    public class ProductItemRepository : IProductItemRepository
    {
        private readonly EcommereceContext _context;
        public ProductItemRepository(EcommereceContext context)
        {
            this._context = context;
        }

        public void AddProductItem(int productId, int quantity , List<int> variationOptions)
        {
            var productItem = new ProductItem() { ProductId = productId, Quantity = quantity };

            if (variationOptions != null && variationOptions.Any())
            {
                // جلب الكيانات الفعلية من قاعدة البيانات بدلاً من إنشاء كائنات جديدة
                productItem.VariationOptions = _context.VariationOptions
                    .Where(vo => variationOptions.Contains(vo.Id)) // تأكد من أن الخيارات موجودة في قاعدة البيانات
                    .ToList();
            }

            _context.ProductItems.Add(productItem);
        }

        public ProductItem GetProductItemByProductId(int productid) => _context.ProductItems.Where(x => x.ProductId == productid).FirstOrDefault();
    }
}
