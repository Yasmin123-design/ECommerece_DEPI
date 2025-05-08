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
	public class VariationOptionServiceTests
	{
		private Mock<IUnitOfWork> _mockUnitOfWork;
		private Mock<IVariationOptionsRepository> _mockVariationOptionRepository;
		private VariationOptionService _variationOptionService;

		[SetUp]
		public void Setup()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockVariationOptionRepository = new Mock<IVariationOptionsRepository>();

			_mockUnitOfWork.Setup(u => u.VariationOptions).Returns(_mockVariationOptionRepository.Object);

			_variationOptionService = new VariationOptionService(_mockUnitOfWork.Object);
		}
		[Test]
		public void CreateVariationOption_ShouldCallRepository()
		{
			var option = new VariationOption();
			_variationOptionService.CreateVariationOption(option);
			_mockVariationOptionRepository.Verify(repo => repo.CreateVariationOption(option), Times.Once);
		}

		[Test]
		public void DeleteVariationOption_ShouldCallRepository()
		{
			var option = new VariationOption();
			_variationOptionService.DeleteVariationOption(option);
			_mockVariationOptionRepository.Verify(repo => repo.DeleteVariationOption(option), Times.Once);
		}

		[Test]
		public void DeleteVariationOptionsByVariationId_ShouldCallRepository()
		{
			int variationId = 1;
			_variationOptionService.DeleteVariationOptionsByVariationId(variationId);
			_mockVariationOptionRepository.Verify(repo => repo.DeleteVariationOptionsByVariationId(variationId), Times.Once);
		}

		[Test]
		public void EditVariationOption_ShouldCallRepository()
		{
			string value = "Red";
			int optionId = 5;
			_variationOptionService.EditVariationOption(value, optionId);
			_mockVariationOptionRepository.Verify(repo => repo.EditVariationOption(value, optionId), Times.Once);
		}

		[Test]
		public void GetByValueAndVariation_ShouldReturnCorrectResult()
		{
			string value = "Small";
			int variationId = 3;
			var expected = new VariationOption();
			_mockVariationOptionRepository.Setup(repo => repo.GetByValueAndVariation(value, variationId)).Returns(expected);
			var result = _variationOptionService.GetByValueAndVariation(value, variationId);
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void GetVariationOptionById_ShouldReturnCorrectResult()
		{
			int id = 2;
			var expected = new VariationOption();
			_mockVariationOptionRepository.Setup(repo => repo.GetVariationOptionById(id)).Returns(expected);
			var result = _variationOptionService.GetVariationOptionById(id);
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void SaveChange_ShouldCallUnitOfWorkSave()
		{
			_variationOptionService.SaveChange();
			_mockUnitOfWork.Verify(u => u.Save(), Times.Once);
		}

		[Test]
		public void UpdateVariationOption_ShouldCallRepository()
		{
			var existing = new VariationOption();
			var updated = new VariationOption();
			_variationOptionService.UpdateVariationOption(existing, updated);
			_mockVariationOptionRepository.Verify(repo => repo.UpdateVariationOption(existing, updated), Times.Once);
		}
	}
}
