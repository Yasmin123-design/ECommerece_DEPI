using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using E_Commerece.Services.Implementations;
using E_Commerece.UnitOfWork.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerce.Tests.ServicesTests
{
	public class WishListServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<IWishlistRepository> _mockWishListRepository;
		private WishlistService _wishlistService;

		[SetUp]
		public void SetUp()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockWishListRepository = new Mock<IWishlistRepository>();
			_mockUnitOfWork.Setup(x => x.Wishlist).Returns(_mockWishListRepository.Object);
			_wishlistService = new WishlistService(_mockUnitOfWork.Object);
		}
		public void GetWishlistByUserId_ShoudReturnWishList()
		{
			var wishlist = new Wishlist();
			_mockWishListRepository.Setup(x => x.GetWishlistByUserId()).Returns(wishlist);
			var result = _wishlistService.GetWishlistByUserId();
			Assert.Equals(result, wishlist);
			_mockWishListRepository.Verify(x => x.GetWishlistByUserId(), Times.Once());

		}
	}

	
}
