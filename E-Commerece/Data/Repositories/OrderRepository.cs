using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using E_Commerece.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerece.Data.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly EcommereceContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
		public OrderRepository(EcommereceContext context)
		{
			this._context = context;
		}
        public void CreateOrder(CheckOutVM model)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new Exception("لم يتم العثور على المستخدم.");
            }

            var cart = _context.Carts.FirstOrDefault(x => x.UserId == userId);
            if (cart == null)
            {
                throw new Exception("السلة غير موجودة.");
            }

            var cartItems = _context.CartItems.Where(x => x.CartId == cart.Id).ToList();
            if (!cartItems.Any())
            {
                throw new Exception("السلة فارغة.");
            }

            var existingOrder = _context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.UserId == userId && o.Status == Status.Preparing);

            decimal totalPrice = 0;

            foreach (var item in cartItems)
            {
                var product = _context.ProductItems
                    .Include(x => x.Product)
                    .FirstOrDefault(p => p.Id == item.ProductItemId);

                if (product != null)
                {
                    totalPrice += item.Quantity * product.Product.Price;
                }
            }

            if (existingOrder != null)
            {
                foreach (var item in cartItems)
                {
                    var existingItem = existingOrder.Items.FirstOrDefault(oi => oi.ProductItemId == item.ProductItemId);

                    if(existingItem == null)
                    {
                        existingOrder.Items.Add(new OrderItem
                        {
                            ProductItemId = item.ProductItemId,
                            Quantity = item.Quantity,
                            OrderId = existingOrder.Id
                        });
                    }
                    else
                    {
                        existingItem.Quantity = item.Quantity;
                    }
                }

                existingOrder.TotalPrice = totalPrice; // ✅ تحديث السعر الإجمالي
            }
            else
            {
                // ✅ إنشاء طلب جديد إذا لم يكن هناك طلب قيد التنفيذ
                existingOrder = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    ArrivalDate = DateTime.Now.AddDays(14),
                    Status = Status.Preparing,
                    TotalPrice = totalPrice,
                    Items = cartItems.Select(item => new OrderItem
                    {
                        ProductItemId = item.ProductItemId,
                        Quantity = item.Quantity
                    }).ToList(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    City = model.City,
                    EmailAddress = model.EmailAddress,
                    Street = model.Street,
                    PhoneNumber = model.PhoneNumber
                };

                _context.Orders.Add(existingOrder);
            }

        }

        public Order GetLatestOrder()
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var order = _context.Orders.Include(x => x.Items)
				.ThenInclude(x => x.ProductItem)
				.ThenInclude(x => x.Product)
				.Where(x => x.UserId == userId)
				.OrderByDescending(o => o.OrderDate)
				.FirstOrDefault();
			return order;
		}

        public Order GetOrderById(int orderid) => _context.Orders.Where(x => x.Id == orderid).FirstOrDefault();

        public List<OrderItem> GetOrderItemsByOrderId(int orderid) => 
            _context.OrderItems.Include(x => x.ProductItem)
            .ThenInclude(x => x.Product).Where(x => x.OrderId == orderid).ToList();
    }
}
