using E_Commerece.Data.Interfaces;
using E_Commerece.Services.Implementations;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;
using Moq;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tests.ServicesTests
{
    public class NewsletterSubscribersServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<INewsletterSubscribersRepository> _mockNewsletterSubscribersRepository;
        private INewsletterSubscribersService _newsletterSubscribersService;

        [SetUp]
        public void SetUp()
        {
            _mockNewsletterSubscribersRepository = new Mock<INewsletterSubscribersRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(x => x.NewsletterSubscribers).Returns(_mockNewsletterSubscribersRepository.Object);

            _newsletterSubscribersService = new NewsletterSubscribersService(_mockUnitOfWork.Object);
        }

        [Test]
        public void CreateNew_ShouldCallRepositoryCreateNew()
        {
            string testEmail = "test@email.com";

            // Act
            _newsletterSubscribersService.CreateNew(testEmail);

            // Assert
            _mockNewsletterSubscribersRepository.Verify(r => r.CreateNew(testEmail), Times.Once);
        }
        [Test]
        public void EmailExists_ShouldReturnCorrectValue()
        {
            string email = "test@email.com";
            _mockNewsletterSubscribersRepository.Setup(r => r.EmailExists(email)).Returns(true);
            // Act
            var result = _newsletterSubscribersService.EmailExists(email);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
