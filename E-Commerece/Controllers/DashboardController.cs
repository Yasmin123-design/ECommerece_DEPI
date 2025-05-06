using E_Commerece.Services.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerece.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProductService _productService;
        public DashboardController(IProductService productService)
        {
            this._productService = productService;
        }
        public IActionResult DashBoard()
        {
            var purchasedProducts = _productService.GetLatestPurchasedProducts().Take(5).ToList();
            var soldProducts = _productService.GetLatestSoldProducts().Take(5).ToList();
            var requestedProducts = _productService.GetRequestedItemsForSeller().Take(5).ToList();

            var model = new DashBoardVM
            {
                PurchasedProducts = purchasedProducts,
                SoldchasedProducts = soldProducts,
                RequestProducts = requestedProducts
            };
            return View(model);
        }

        public IActionResult AllSoldProducts()
        {
            var allsoldproducts = _productService.GetLatestSoldProducts();
            return View(allsoldproducts);
        }
        public IActionResult AllRequestOrders()
        {
            var allrequestorders = _productService.GetRequestedItemsForSeller();
            return View(allrequestorders);
        }
   
      
		public IActionResult AllPurchasedProducts()
        {
            var allpurchasedproducts = _productService.GetLatestPurchasedProducts();
            return View(allpurchasedproducts);
        }


    }
}
