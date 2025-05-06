using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using E_Commerece.Services.Implementations;
using E_Commerece.UnitOfWork.Interfaces;
using E_Commerece.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tests.ServicesTests
{
	public class OrderServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<IOrderRepository> _mockOrderRepository;
		private OrderService _OrderService;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockOrderRepository = new Mock<IOrderRepository>();

			_mockUnitOfWork.Setup(u => u.Orders).Returns(_mockOrderRepository.Object);

			_OrderService = new OrderService(_mockUnitOfWork.Object);
		}
		[Test]
		public void CreateOrder_ShouldCallRepository_WithCorrectModel()
		{
			// Arrange
			var model = new CheckOutVM
			{
				// املأ القيم حسب الحاجة للاختبار
				FirstName = "Yasmin",
				LastName = "Elabasy",
				City="Cairo",
				EmailAddress="email85@gmail.com",
				PhoneNumber="13456789",
				Street="Elshabka"
			};

			// Act
			_OrderService.CreateOrder(model);

			// Assert
			_mockOrderRepository.Verify(repo => repo.CreateOrder(model), Times.Once);
		}
		[Test]
		public void AllOrderes_ShouldReturnOrders()
		{
			var expectedOrders = new List<Order> { new Order(), new Order() };
			_mockOrderRepository.Setup(r => r.AllOrderes()).Returns(expectedOrders);

			var result = _OrderService.AllOrderes();

			Assert.AreEqual(expectedOrders, result);
			_mockOrderRepository.Verify(repo => repo.AllOrderes(), Times.Once);

		}

		[Test]
		public void GetCartOrderDetails_ShouldReturnDetails()
		{
			var expected = new OrderDetailsVM { TotalPrice = 100 };
			_mockOrderRepository.Setup(r => r.GetCartOrderDetails()).Returns(expected);

			var result = _OrderService.GetCartOrderDetails();

			Assert.AreEqual(expected.TotalPrice, result.TotalPrice);
			_mockOrderRepository.Verify(repo => repo.GetCartOrderDetails(), Times.Once);

		}

		[Test]
		public void GetLatestOrder_ShouldReturnLatestOrder()
		{
			var latestOrder = new Order { Id = 1 };
			_mockOrderRepository.Setup(r => r.GetLatestOrder()).Returns(latestOrder);

			var result = _OrderService.GetLatestOrder();

			Assert.AreEqual(latestOrder, result);
			_mockOrderRepository.Verify(repo => repo.GetLatestOrder(), Times.Once);

		}

		[Test]
		public void GetOrderById_ShouldReturnCorrectOrder()
		{
			var order = new Order { Id = 5 };
			_mockOrderRepository.Setup(r => r.GetOrderById(5)).Returns(order);

			var result = _OrderService.GetOrderById(5);

			Assert.AreEqual(order, result);
			_mockOrderRepository.Verify(repo => repo.GetOrderById(5), Times.Once);

		}

		[Test]
		public void GetOrderCount_ShouldReturnCorrectCount()
		{
			_mockOrderRepository.Setup(r => r.GetOrderCount()).Returns(10);

			var result = _OrderService.GetOrderCount();

			Assert.AreEqual(10, result);
			_mockOrderRepository.Verify(repo => repo.GetOrderCount(), Times.Once);

		}

		[Test]
		public void GetOrderItemsByOrderId_ShouldReturnItems()
		{
			var items = new List<OrderItem> { new OrderItem(), new OrderItem() };
			_mockOrderRepository.Setup(r => r.GetOrderItemsByOrderId(2)).Returns(items);

			var result = _OrderService.GetOrderItemsByOrderId(2);

			Assert.AreEqual(2, result.Count);
			_mockOrderRepository.Verify(repo => repo.GetOrderItemsByOrderId(2), Times.Once);

		}

		[Test]
		public void SalesPerMonth_ShouldReturnSalesData()
		{
			var sales = new List<SalePerMonthVM> { new SalePerMonthVM(), new SalePerMonthVM() };
			_mockOrderRepository.Setup(r => r.SalesPerMonth()).Returns(sales);

			var result = _OrderService.SalesPerMonth();

			Assert.AreEqual(2, result.Count);
			_mockOrderRepository.Verify(repo => repo.SalesPerMonth(), Times.Once);

		}

		[Test]
		public void SaveChange_ShouldCallUnitOfWorkSave()
		{
			_OrderService.SaveChange();
			_mockUnitOfWork.Verify(u => u.Save(), Times.Once);
		}
	}
}
