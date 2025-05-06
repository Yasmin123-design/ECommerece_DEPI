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
	public class ProfileServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<IProfileRepository> _mockProfileRepository;
		private ProfileService _ProfileService;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockProfileRepository = new Mock<IProfileRepository>();

			_mockUnitOfWork.Setup(u => u.Profile).Returns(_mockProfileRepository.Object);

			_ProfileService = new ProfileService(_mockUnitOfWork.Object);
		}
		[Test]
		public void EditUserInfo_ShouldCallEditUserInfoOnce()
		{
			// Arrange
			var user = new User { Fname="yssmin",Id="ghjkl977" }; // إنشاء كائن User للاختبار
			_mockProfileRepository.Setup(repo => repo.EditUserInfo(user));

			// Act
			_ProfileService.EditUserInfo(user);

			// Assert
			_mockProfileRepository.Verify(repo => repo.EditUserInfo(user), Times.Once); // تأكيد استدعاء الدالة مرة واحدة
		}
		[Test]
		public void GetUserById_ShouldReturnUser()
		{
			// Arrange
			var expectedUser = new User { Fname = "yssmin", Id = "ghjkl977" };
			_mockProfileRepository.Setup(repo => repo.GetUserById()).Returns(expectedUser);

			// Act
			var result = _ProfileService.GetUserById();

			// Assert
			Assert.AreEqual(expectedUser, result); // التأكد من أن النتيجة هي نفس الكائن الذي تم إرجاعه من الـ repository
		}
		[Test]
		public void SaveChange_ShouldCallSaveOnce()
		{
			// Arrange
			_mockUnitOfWork.Setup(u => u.Save());

			// Act
			_ProfileService.SaveChange();

			// Assert
			_mockUnitOfWork.Verify(u => u.Save(), Times.Once); // التأكد من استدعاء Save مرة واحدة
		}


	}
}
