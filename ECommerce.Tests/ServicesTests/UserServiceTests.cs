using E_Commerece.Data.Interfaces;
using E_Commerece.Models;
using E_Commerece.Services.Implementations;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;
using E_Commerece.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ECommerce.Tests.ServicesTests
{
	public class UserServiceTests
	{
		private Mock<UserManager<User>> _mockUserManager;
		private Mock<SignInManager<User>> _mockSignInManager;
		private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
		private Mock<IEmailService> _mockEmailService;
		private Mock<IConfiguration> _mockConfiguration;
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<IUserRepository> _mockUserRepo;

		private UserService _userService;

		[SetUp]
		public void SetUp()
		{
			var userStoreMock = new Mock<IUserStore<User>>();
			_mockUserManager = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

			var contextAccessor = new Mock<IHttpContextAccessor>();
			var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
			_mockSignInManager = new Mock<SignInManager<User>>(_mockUserManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null, null);

			_mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
			_mockEmailService = new Mock<IEmailService>();
			_mockConfiguration = new Mock<IConfiguration>();
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockUserRepo = new Mock<IUserRepository>();

			_mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);

			_userService = new UserService(
				_mockUserManager.Object,
				_mockSignInManager.Object,
				_mockHttpContextAccessor.Object,
				_mockEmailService.Object,
				_mockConfiguration.Object,
				_mockUnitOfWork.Object
			);
		}

		[Test]
		public void GetUserCount_ShouldReturnCorrectCount()
		{
			_mockUserRepo.Setup(r => r.GetUserCount()).Returns(5);

			var result = _userService.GetUserCount();

			Assert.AreEqual(5, result);
			_mockUserRepo.Verify(r => r.GetUserCount(), Times.Once);
		}

		[Test]
		public async Task LoginAsync_WithValidCredentials_ShouldReturnTrue()
		{
			var loginVM = new LoginVM { Email = "test@email.com", Password = "Password123", RememberMe = false };
			var user = new User { Email = loginVM.Email };

			_mockUserManager.Setup(u => u.FindByEmailAsync(loginVM.Email)).ReturnsAsync(user);
			_mockSignInManager.Setup(s => s.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false)).ReturnsAsync(SignInResult.Success);

			var result = await _userService.LoginAsync(loginVM);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task RegisterAsync_ShouldAssignCorrectRole()
		{
			var registerVM = new RegisterVM
			{
				Email = "user@email.com",
				Password = "Password123",
				UserName = "username",
				Address = "address"
			};

			_mockUserManager.Setup(u => u.CreateAsync(It.IsAny<User>(), registerVM.Password))
				.ReturnsAsync(IdentityResult.Success);

			var sectionMock = new Mock<IConfigurationSection>();
			sectionMock.Setup(s => s.Value).Returns("admin1@email.com,admin2@email.com");

			_mockConfiguration.Setup(c => c.GetSection("AdminSettings:AdminEmails"))
				.Returns(sectionMock.Object);

			_mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<User>(), "User"))
				.ReturnsAsync(IdentityResult.Success);

			var result = await _userService.RegisterAsync(registerVM);

			Assert.IsTrue(result.Succeeded);
		}


		[Test]
		public async Task SendPasswordResetEmailAsync_UserExists_ShouldSendEmail()
		{
			var email = "test@email.com";
			var user = new User { Email = email };
			var token = "reset-token";

			_mockUserManager.Setup(u => u.FindByEmailAsync(email)).ReturnsAsync(user);
			_mockUserManager.Setup(u => u.GeneratePasswordResetTokenAsync(user)).ReturnsAsync(token);

			_mockEmailService.Setup(e => e.SendEmailAsync(email, It.IsAny<string>(), It.Is<string>(s => s.Contains(token)))).Returns(Task.CompletedTask);

			var result = await _userService.SendPasswordResetEmailAsync(email);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task ResetPasswordAsync_ShouldReturnTrue_WhenSuccess()
		{
			var model = new RestPasswordVM
			{
				Email = "test@email.com",
				Token = "token",
				NewPassword = "NewPass123"
			};

			var user = new User { Email = model.Email };

			_mockUserManager.Setup(u => u.FindByEmailAsync(model.Email)).ReturnsAsync(user);
			_mockUserManager.Setup(u => u.ResetPasswordAsync(user, model.Token, model.NewPassword)).ReturnsAsync(IdentityResult.Success);

			var result = await _userService.ResetPasswordAsync(model);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task GetUserRoleAsync_ShouldReturnRole()
		{
			var email = "test@email.com";
			var user = new User { Email = email };

			_mockUserManager.Setup(u => u.FindByEmailAsync(email)).ReturnsAsync(user);
			_mockUserManager.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });

			var role = await _userService.GetUserRoleAsync(email);

			Assert.AreEqual("User", role);
		}
	}
}
