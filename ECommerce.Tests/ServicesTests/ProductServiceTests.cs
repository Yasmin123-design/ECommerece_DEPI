using E_Commerece.Data.Interfaces;
using E_Commerece.Services.Implementations;
using E_Commerece.UnitOfWork.Interfaces;
using Moq;
using E_Commerece.Models;

namespace ECommerce.Tests.ServicesTests
{
	public class ProductServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<IProductRepository> _mockProductRepository;
		private ProductService _productService;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockProductRepository = new Mock<IProductRepository>();

			_mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);

			_productService = new ProductService(_mockUnitOfWork.Object);
		}
		[Test]
		public void AddReviewOnProduct_ShouldCallRepository_AddReviewOnProduct()
		{
			// Arrange
			var productId = 1;
			var comment = "Great product!";
			var rating = 5;

			// Act
			_productService.AddReviewOnProduct(productId, comment, rating);

			// Assert
			_mockProductRepository.Verify(repo => repo.AddReviewOnProduct(productId, comment, rating), Times.Once);
		}
		[Test]
		public void GetAllProducts_ShouldReturnListOfProducts()
		{
			// Arrange
			var products = new List<Product>
	{
		new Product { Id = 1, Name = "Product 1" },
		new Product { Id = 2, Name = "Product 2" }
	};

			_mockProductRepository.Setup(repo => repo.GetAllProducts()).Returns(products);

			// Act
			var result = _productService.GetAllProducts();

			// Assert
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual("Product 1", result[0].Name);
			_mockProductRepository.Verify(repo => repo.GetAllProducts(), Times.Once);
		}
		[Test]
		public void GetProductById_ShouldReturnProduct()
		{
			// Arrange
			var productId = 1;
			var product = new Product { Id = productId, Name = "Product 1" };

			_mockProductRepository.Setup(repo => repo.GetProductById(productId)).Returns(product);

			// Act
			var result = _productService.GetProductById(productId);

			// Assert
			Assert.AreEqual(productId, result.Id);
			Assert.AreEqual("Product 1", result.Name);
			_mockProductRepository.Verify(repo => repo.GetProductById(productId), Times.Once);
		}
		[Test]
		public void CreateProductByAdmin_ShouldCallCreateProductByAdmin()
		{
			// Arrange
			var product = new Product { Id = 1, Name = "New Product" };

			// Act
			_productService.CreateProductByAdmin(product);

			// Assert
			_mockProductRepository.Verify(repo => repo.CreateProductByAdmin(product), Times.Once);
		}
		[Test]
		public void DeleteProductFromWishlist_ShouldCallDeleteProductFromWishlist()
		{
			// Arrange
			var productId = 1;

			// Act
			_productService.DeleteProductFromWishlist(productId);

			// Assert
			_mockProductRepository.Verify(repo => repo.DeleteProductFromWishlist(productId), Times.Once);
		}
		[Test]
		public void GetAllPenddingProuct_ShouldReturnListOfPendingProducts()
		{
			// Arrange
			var pendingProducts = new List<Product>
	{
		new Product { Id = 1, Name = "Pending Product 1" },
		new Product { Id = 2, Name = "Pending Product 2" }
	};

			_mockProductRepository.Setup(repo => repo.GetAllPenddingProuct()).Returns(pendingProducts);

			// Act
			var result = _productService.GetAllPenddingProuct();

			// Assert
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual("Pending Product 1", result[0].Name);
			_mockProductRepository.Verify(repo => repo.GetAllPenddingProuct(), Times.Once);
		}
		[Test]
		public void GetProductCount_ShouldReturnProductCount()
		{
			// Arrange
			var expectedCount = 10;

			_mockProductRepository.Setup(repo => repo.GetProductCount()).Returns(expectedCount);

			// Act
			var result = _productService.GetProductCount();

			// Assert
			Assert.AreEqual(expectedCount, result);
			_mockProductRepository.Verify(repo => repo.GetProductCount(), Times.Once);
		}
		[Test]
		public void GetTopRatedProducts_ShouldReturnTopRatedProducts()
		{
			// Arrange
			var topRatedProducts = new List<object>
	{
		new { Id = 1, Name = "Top Rated Product 1" },
		new { Id = 2, Name = "Top Rated Product 2" }
	};

			_mockProductRepository.Setup(repo => repo.GetTopRatedProducts()).Returns(topRatedProducts);

			// Act
			var result = _productService.GetTopRatedProducts();

			// Assert
			Assert.AreEqual(2, result.Count);
			_mockProductRepository.Verify(repo => repo.GetTopRatedProducts(), Times.Once);
		}

		[Test]
		public void ApprovedProduct_ShouldCallApprovedProduct()
		{
			// Arrange
			var productId = 1;

			// Act
			_productService.ApprovedProduct(productId);

			// Assert
			_mockProductRepository.Verify(repo => repo.ApprovedProduct(productId), Times.Once);
		}

		[Test]
		public void RejectProduct_ShouldCallRejectProduct()
		{
			// Arrange
			var productId = 1;

			// Act
			_productService.RejectProduct(productId);

			// Assert
			_mockProductRepository.Verify(repo => repo.RejectProduct(productId), Times.Once);
		}

		[Test]
		public void DeleteProductFromSelledPrds_ShouldCallDeleteProductFromSelledPrds()
		{
			// Arrange
			var product = new Product { Id = 1, Name = "Product to Delete" };

			// Act
			_productService.DeleteProductFromSelledPrds(product);

			// Assert
			_mockProductRepository.Verify(repo => repo.DeleteProductFromSelledPrds(product), Times.Once);
		}

		[Test]
		public void UpdateProduct_ShouldCallUpdateProduct()
		{
			// Arrange
			var oldProduct = new Product { Id = 1, Name = "Old Product" };
			var newProduct = new Product { Id = 1, Name = "Updated Product" };

			// Act
			_productService.UpdateProduct(oldProduct, newProduct);

			// Assert
			_mockProductRepository.Verify(repo => repo.UpdateProduct(oldProduct, newProduct), Times.Once);
		}

		[Test]
		public void CreateSelledProductByUser_ShouldCallCreateSelledProductByUser()
		{
			// Arrange
			var product = new Product { Id = 1, Name = "New Selled Product" };

			// Act
			_productService.CreateSelledProductByUser(product);

			// Assert
			_mockProductRepository.Verify(repo => repo.CreateSelledProductByUser(product), Times.Once);
		}
		[Test]
		public void GetLatestPurchasedProducts_ShouldReturnOrders()
		{
			var orders = new List<Order> { new Order { Id = 1 } };
			_mockUnitOfWork.Setup(u => u.Products.GetLatestPurchasedProducts()).Returns(orders);

			var result = _productService.GetLatestPurchasedProducts();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void GetLatestSoldProducts_ShouldReturnProducts()
		{
			var products = new List<Product> { new Product { Id = 1 } };
			_mockUnitOfWork.Setup(u => u.Products.GetLatestSoldProducts()).Returns(products);

			var result = _productService.GetLatestSoldProducts();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void GetRequestedItemsForSeller_ShouldReturnOrderItems()
		{
			var items = new List<OrderItem> { new OrderItem { Id = 1 } };
			_mockUnitOfWork.Setup(u => u.Products.GetRequestedItemsForSeller()).Returns(items);

			var result = _productService.GetRequestedItemsForSeller();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void GetAllPenddingProuct_ShouldReturnPendingProducts()
		{
			var products = new List<Product> { new Product { Id = 1 } };
			_mockUnitOfWork.Setup(u => u.Products.GetAllPenddingProuct()).Returns(products);

			var result = _productService.GetAllPenddingProuct();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void ApprovedProduct_ShouldCallRepository()
		{
			_productService.ApprovedProduct(1);
			_mockUnitOfWork.Verify(u => u.Products.ApprovedProduct(1), Times.Once);
		}

		[Test]
		public void ApprovedAllProducts_ShouldCallRepository()
		{
			_productService.ApprovedAllProducts();
			_mockUnitOfWork.Verify(u => u.Products.ApprovedAllProducts(), Times.Once);
		}

		[Test]
		public void RejectAllProducts_ShouldCallRepository()
		{
			_productService.RejectAllProducts();
			_mockUnitOfWork.Verify(u => u.Products.RejectAllProducts(), Times.Once);
		}

		[Test]
		public void RejectProduct_ShouldCallRepository()
		{
			_productService.RejectProduct(1);
			_mockUnitOfWork.Verify(u => u.Products.RejectProduct(1), Times.Once);
		}

		[Test]
		public void GetProductCount_ShouldReturnCount()
		{
			_mockUnitOfWork.Setup(u => u.Products.GetProductCount()).Returns(5);
			var result = _productService.GetProductCount();
			Assert.AreEqual(5, result);
		}

		[Test]
		public void AllPrdsToManage_ShouldReturnProducts()
		{
			var products = new List<Product> { new Product { Id = 1 } };
			_mockUnitOfWork.Setup(u => u.Products.AllPrdsToManage()).Returns(products);

			var result = _productService.AllPrdsToManage();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void FilteredProductByCategoryId_ShouldReturnFilteredProducts()
		{
			var products = new List<Product> { new Product { Id = 1 } };
			_mockUnitOfWork.Setup(u => u.Products.FilteredProductByCategoryId(1)).Returns(products);

			var result = _productService.FilteredProductByCategoryId(1);

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void GetProductsByCategoryIdJsonReturnedForAdmin_ShouldReturnList()
		{
			var resultList = new List<object> { new { Id = 1, Name = "Test" } };
			var categoryIds = new List<int> { 1, 2 };

			_mockUnitOfWork.Setup(u => u.Products.GetProductsByCategoryIdJsonReturnedForAdmin(categoryIds)).Returns(resultList);

			var result = _productService.GetProductsByCategoryIdJsonReturnedForAdmin(categoryIds);

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void CreateProductByAdmin_ShouldCallRepository()
		{
			var product = new Product { Id = 1 };
			_productService.CreateProductByAdmin(product);
			_mockUnitOfWork.Verify(u => u.Products.CreateProductByAdmin(product), Times.Once);
		}

		[Test]
		public void DeletedProduct_ShouldCallRepository()
		{
			var product = new Product { Id = 1 };
			_productService.DeletedProduct(product);
			_mockUnitOfWork.Verify(u => u.Products.DeletedProduct(product), Times.Once);
		}

		[Test]
		public void UpdateProduct_ShouldCallRepository()
		{
			var oldProduct = new Product { Id = 1, Name = "Old" };
			var newProduct = new Product { Id = 1, Name = "New" };

			_productService.UpdateProduct(oldProduct, newProduct);

			_mockUnitOfWork.Verify(u => u.Products.UpdateProduct(oldProduct, newProduct), Times.Once);
		}

		[Test]
		public void GetSmartPhoneProducts_ShouldReturnList()
		{
			var products = new List<Product> { new Product { Id = 1, Name = "Phone" } };
			_mockUnitOfWork.Setup(u => u.Products.GetSmartPhoneProducts()).Returns(products);

			var result = _productService.GetSmartPhoneProducts();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void SeveChanges_ShouldCallUnitOfWorkSave()
		{
			_productService.SeveChanges();
			_mockUnitOfWork.Verify(u => u.Save(), Times.Once);
		}

		[Test]
		public void GetProductsByPage_ShouldReturnPagedProducts()
		{
			int pageNumber = 2;
			var pagedProducts = new List<object> { new { Id = 1, Name = "PagedProduct" } };
			_mockUnitOfWork.Setup(u => u.Products.GetProductsByPage(pageNumber)).Returns(pagedProducts);

			var result = _productService.GetProductsByPage(pageNumber);

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void GetProductsByCategoryIdJsonReturnedForUser_ShouldReturnFilteredList()
		{
			var categoryIds = new List<int> { 1, 2 };
			var expected = new List<object> { new { Id = 1, Name = "UserProduct" } };
			_mockUnitOfWork.Setup(u => u.Products.GetProductsByCategoryIdJsonReturnedForUser(categoryIds)).Returns(expected);

			var result = _productService.GetProductsByCategoryIdJsonReturnedForUser(categoryIds);

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void CheckAddProductToCart_ShouldReturnProductItem()
		{
			int productId = 1;
			var optionIds = new List<int> { 5, 6 };
			var expectedItem = new ProductItem { ProductId = 1 };

			_mockUnitOfWork.Setup(u => u.Products.CheckAddProductToCart(productId, optionIds)).Returns(expectedItem);

			var result = _productService.CheckAddProductToCart(productId, optionIds);

			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.ProductId);
		}
		[Test]
		public void GetAccessoriesProducts_ShouldReturnAccessoriesList()
		{
			var accessories = new List<Product> { new Product { Id = 1, Name = "Accessory" } };
			_mockUnitOfWork.Setup(u => u.Products.GetAccessoriesProducts()).Returns(accessories);

			var result = _productService.GetAccessoriesProducts();

			Assert.AreEqual(1, result.Count);
			Assert.AreEqual("Accessory", result[0].Name);
		}

		[Test]
		public void GetAllProductsStartWith_ShouldReturnMatchingProducts()
		{
			string query = "Lap";
			var products = new List<Product>
	{
		new Product { Id = 1, Name = "Laptop 1" },
		new Product { Id = 2, Name = "Laptop 2" }
	};
			_mockUnitOfWork.Setup(u => u.Products.GetAllProductsStartWith(query)).Returns(products);

			var result = _productService.GetAllProductsStartWith(query);

			Assert.AreEqual(2, result.Count);
		}

		[Test]
		public void GetAvailableQuantity_ShouldReturnCorrectQuantity()
		{
			int productId = 1;
			var optionIds = new List<int> { 10, 20 };
			_mockUnitOfWork.Setup(u => u.Products.GetAvailableQuantity(productId, optionIds)).Returns(5);

			var result = _productService.GetAvailableQuantity(productId, optionIds);

			Assert.AreEqual(5, result);
		}

		[Test]
		public void GetCameraProducts_ShouldReturnCameraList()
		{
			var cameras = new List<Product> { new Product { Id = 1, Name = "Camera" } };
			_mockUnitOfWork.Setup(u => u.Products.GetCameraProducts()).Returns(cameras);

			var result = _productService.GetCameraProducts();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void GetLaptopsProducts_ShouldReturnLaptopsList()
		{
			var laptops = new List<Product> { new Product { Id = 1, Name = "Laptop" } };
			_mockUnitOfWork.Setup(u => u.Products.GetLaptopsProducts()).Returns(laptops);

			var result = _productService.GetLaptopsProducts();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void GetAllProductsJsonReturned_ShouldReturnListOfObjects()
		{
			var expectedProducts = new List<object> { new { Id = 1, Name = "Product A" } };
			_mockUnitOfWork.Setup(u => u.Products.GetAllProductsJsonReturned()).Returns(expectedProducts);

			var result = _productService.GetAllProductsJsonReturned();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void AddProductToWishlist_ShouldReturnTrue_WhenSuccessful()
		{
			int productId = 1;
			_mockUnitOfWork.Setup(u => u.Products.AddProductToWishlist(productId)).Returns(true);

			var result = _productService.AddProductToWishlist(productId);

			Assert.IsTrue(result.Value);
		}

		[Test]
		public void AddProductToWishlist_ShouldReturnFalse_WhenFails()
		{
			int productId = 1;
			_mockUnitOfWork.Setup(u => u.Products.AddProductToWishlist(productId)).Returns(false);

			var result = _productService.AddProductToWishlist(productId);

			Assert.IsFalse(result.Value);
		}

		[Test]
		public void ShowAllProductsSelledBySpecificUser_ShouldReturnProductList()
		{
			var expectedProducts = new List<Product> { new Product { Id = 1, Name = "Product B" } };
			_mockUnitOfWork.Setup(u => u.Products.ShowAllProductsSelledBySpecificUser()).Returns(expectedProducts);

			var result = _productService.ShowAllProductsSelledBySpecificUser();

			Assert.AreEqual(1, result.Count);
		}

		[Test]
		public void GetCategoryIdByProductId_ShouldReturnCorrectCategoryId()
		{
			int productId = 5;
			int expectedCategoryId = 3;
			_mockUnitOfWork.Setup(u => u.Products.GetCategoryIdByProductId(productId)).Returns(expectedCategoryId);

			var result = _productService.GetCategoryIdByProductId(productId);

			Assert.AreEqual(expectedCategoryId, result);
		}



	}
}
