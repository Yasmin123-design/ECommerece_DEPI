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

namespace ECommerce.Tests.ServicesTests
{
	public class CategoryServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<ICategoryRepository> _mockCategoryRepository;
		private CategoryService _CategoryService;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockCategoryRepository = new Mock<ICategoryRepository>();

			_mockUnitOfWork.Setup(u => u.Categories).Returns(_mockCategoryRepository.Object);

			_CategoryService = new CategoryService(_mockUnitOfWork.Object);
		}

		[Test]
		public void GetAllCategories_ShouldReturnAllCategories()
		{
			// Arrange
			var expectedCategories = new List<Category>
	{
		new Category { Id = 1, Name = "T-Shirts" },
		new Category { Id = 2, Name = "Pants" }
	};

			_mockCategoryRepository.Setup(r => r.GetAllCategories()).Returns(expectedCategories);

			// Act
			var result = _CategoryService.GetAllCategories();

			// Assert
			Assert.AreEqual(expectedCategories.Count, result.Count);
			Assert.AreEqual(expectedCategories[0].Name, result[0].Name);
			_mockCategoryRepository.Verify(r => r.GetAllCategories(), Times.Once);
		}

		[Test]
		public void GetCategory_ShouldReturnCorrectCategory()
		{
			// Arrange
			var categoryId = 1;
			var expectedCategory = new Category { Id = categoryId, Name = "Accessories" };

			_mockCategoryRepository.Setup(r => r.GetCategory(categoryId)).Returns(expectedCategory);

			// Act
			var result = _CategoryService.GetCategory(categoryId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedCategory.Name, result.Name);
			_mockCategoryRepository.Verify(r => r.GetCategory(categoryId), Times.Once);
		}

		[Test]
		public void Create_ShouldCallRepositoryCreate()
		{
			// Arrange
			var newCategory = new Category { Id = 3, Name = "Shorts" };

			// Act
			_CategoryService.Create(newCategory);

			// Assert
			_mockCategoryRepository.Verify(r => r.Create(newCategory), Times.Once);
		}


		[Test]
		public void Delete_ShouldCallRepositoryDelete()
		{
			// Arrange
			var categoryToDelete = new Category { Id = 2, Name = "Pants" };

			// Act
			_CategoryService.Delete(categoryToDelete);

			// Assert
			_mockCategoryRepository.Verify(r => r.Delete(categoryToDelete), Times.Once);
		}

		[Test]
		public void GetCategoryCount_ShouldReturnCorrectCount()
		{
			// Arrange
			_mockCategoryRepository.Setup(r => r.GetCategoryCount()).Returns(5);

			// Act
			var count = _CategoryService.GetCategoryCount();

			// Assert
			Assert.AreEqual(5, count);
			_mockCategoryRepository.Verify(r => r.GetCategoryCount(), Times.Once);
		}

		[Test]
		public void SaveChange_ShouldCallUnitOfWorkSave()
		{
			_mockUnitOfWork.Setup(r => r.Save()).Verifiable();
			// Act
			_CategoryService.SaveChange();

			// Assert
			_mockUnitOfWork.Verify(u => u.Save(), Times.Once);
		}

	}
}
