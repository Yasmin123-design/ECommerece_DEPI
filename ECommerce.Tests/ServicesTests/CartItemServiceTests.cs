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
	public class CartItemServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<ICartItemRepository> _mockCartItemRepository;
		private CartItemService _cartItemService;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockCartItemRepository = new Mock<ICartItemRepository>();

			_mockUnitOfWork.Setup(u => u.CartItems).Returns(_mockCartItemRepository.Object);

			_cartItemService = new CartItemService(_mockUnitOfWork.Object);
		}
		[Test]
		public void AddCartItem_ShouldCallRepositoryAndReturnTrue()
		{
			var productId = 1;
			var productItem = new ProductItem { Id = 1, Quantity = 5 };
			var quantity = 2;

			_mockCartItemRepository
				.Setup(r => r.AddCartItem(productId, productItem, quantity))
				.Returns(true);

			var result = _cartItemService.AddCartItem(productId, productItem, quantity);

			Assert.IsTrue(result);

			_mockCartItemRepository.Verify(r => r.AddCartItem(productId, productItem, quantity), Times.Once);
		}

		[Test]
		public void GetCartItem_ShouldReturnCartItem()
		{
			var itemid = 1;
			var expectedCartItem = new CartItem { Id = itemid};


			_mockCartItemRepository
				.Setup(r => r.GetCartItem(itemid))
				.Returns(expectedCartItem);

			var result = _cartItemService.GetCartItem(itemid);
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedCartItem.Id, result.Id);
			_mockCartItemRepository.Verify(r => r.GetCartItem(itemid), Times.Once);
		}

		[Test]
		public void RemoveCartItem_ShouldCallRepositoryRemoveCartItem()
		{
			var id = 1;
			_mockCartItemRepository.Setup(r => r.RemoveCartItem(id)).Verifiable();
			_cartItemService.RemoveCartItem(id);
			_mockCartItemRepository.Verify(r => r.RemoveCartItem(id), Times.Once);

		}
		[Test]
		public void RemoveAllItemsRelatedByCart_ShouldCallRepositoryRemoveAllItems()
		{
			var cartId = 1;
			_mockCartItemRepository.Setup(r => r.RemoveAllItemsRelatedByCart(cartId)).Verifiable();

			_cartItemService.RemoveAllItemsRelatedByCart(cartId);

			_mockCartItemRepository.Verify(r => r.RemoveAllItemsRelatedByCart(cartId), Times.Once);
		}

		[Test]
		public void SaveChange_ShouldCallUnitOfWorkSave()
		{
			_mockUnitOfWork.Setup(u => u.Save()).Verifiable();

			_cartItemService.SeveChanges();

			_mockUnitOfWork.Verify(u => u.Save(), Times.Once);
		}



	}
}
