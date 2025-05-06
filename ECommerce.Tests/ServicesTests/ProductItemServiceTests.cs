using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using E_Commerece.Services.Implementations;
using E_Commerece.UnitOfWork.Interfaces;
using Microsoft.Build.Evaluation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tests.ServicesTests
{
	public class ProductItemServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<IProductItemRepository> _mockProductItemRepository;
		private ProductItemService _ProductItemService;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockProductItemRepository = new Mock<IProductItemRepository>();

			_mockUnitOfWork.Setup(u => u.ProductItems).Returns(_mockProductItemRepository.Object);

			_ProductItemService = new ProductItemService(_mockUnitOfWork.Object);
		}
		[Test]
		public void AddProductItem_ShouldCallAddProductItem()
		{
			// Arrange
			var productId = 1;
			var quantity = 5;
			var variationOptions = new List<int> { 1, 2 };

			_mockProductItemRepository.Setup(repo => repo.AddProductItem(productId, quantity, variationOptions));

			// Act
			_ProductItemService.AddProductItem(productId, quantity, variationOptions);

			// Assert
			_mockProductItemRepository.Verify(repo => repo.AddProductItem(productId, quantity, variationOptions), Times.Once);
		}
		[Test]
		public void DeletePrdItem_ShouldCallDeletePrdItem()
		{
			// Arrange
			var productItem = new ProductItem { Id = 1 };  // مثال على الكائن اللي هيتحذف

			_mockProductItemRepository.Setup(repo => repo.DeletePrdItem(productItem));

			// Act
			_ProductItemService.DeletePrdItem(productItem);

			// Assert
			_mockProductItemRepository.Verify(repo => repo.DeletePrdItem(productItem), Times.Once);
		}
		[Test]
		public void GetProductItemById_ShouldReturnProductItem()
		{
			// Arrange
			var productId = 1;
			var expectedProductItem = new ProductItem { Id = productId };
			_mockProductItemRepository.Setup(repo => repo.GetProductItemById(productId)).Returns(expectedProductItem);

			// Act
			var result = _ProductItemService.GetProductItemById(productId);

			// Assert
			Assert.AreEqual(expectedProductItem, result);
			_mockProductItemRepository.Verify(repo => repo.GetProductItemById(productId), Times.Once);

		}
		[Test]
		public void GetProductItemByProductId_ShouldReturnProductItem()
		{
			// Arrange
			var productId = 1;
			var expectedProductItem = new ProductItem { ProductId = productId };
			_mockProductItemRepository.Setup(repo => repo.GetProductItemByProductId(productId)).Returns(expectedProductItem);

			// Act
			var result = _ProductItemService.GetProductItemByProductId(productId);

			// Assert
			Assert.AreEqual(expectedProductItem, result);
			_mockProductItemRepository.Verify(repo => repo.GetProductItemByProductId(productId), Times.Once);

		}
		[Test]
		public void SeveChanges_ShouldCallSave()
		{
			// Arrange
			_mockUnitOfWork.Setup(u => u.Save());

			// Act
			_ProductItemService.SeveChanges();

			// Assert
			_mockUnitOfWork.Verify(u => u.Save(), Times.Once);
		}





	}
}
