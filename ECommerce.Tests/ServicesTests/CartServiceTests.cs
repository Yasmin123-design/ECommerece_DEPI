using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using E_Commerece.Services.Implementations;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tests.ServicesTests
{
	public class CartServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<ICartRepository> _mockCartRepository;
		private CartService _cartService;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockCartRepository = new Mock<ICartRepository>();

			_mockUnitOfWork.Setup(u => u.Carts).Returns(_mockCartRepository.Object);

			_cartService = new CartService(_mockUnitOfWork.Object);
		}

		[Test]
		public void ClearCart_ShouldCallRepositoryClearCart()
		{
			var userid = "dfghjhgf34567654";
			_mockCartRepository.Setup(r => r.ClearCart(userid)).Verifiable();
			_cartService.ClearCart(userid);
			_mockCartRepository.Verify(r => r.ClearCart(userid), Times.Once);
		}
		[Test]
		public void GetCartById_ShouldReturnCorrectCart()
		{
			var id = 1;
			var price = 567;
			var expectedCart = new Cart { Id = id , TotalPrice = price  };
			_mockCartRepository.Setup(r => r.GetCartById(id)).Returns(expectedCart);
			var result = _cartService.GetCartById(id);
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedCart.Id, result.Id);
			Assert.AreEqual(expectedCart.TotalPrice, result.TotalPrice);
			_mockCartRepository.Verify(r => r.GetCartById(id), Times.Once);

		}
		[Test]
		public void SaveChange_ShouldCallUnitOfWorkSave()
		{
			_mockUnitOfWork.Setup(u => u.Save()).Verifiable();

			_cartService.SaveChange();

			_mockUnitOfWork.Verify(u => u.Save(), Times.Once);
		}
	}
}
