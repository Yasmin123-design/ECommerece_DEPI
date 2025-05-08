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
	public class VariationServiceTests
	{
		private  Mock<IUnitOfWork> _mockUnitOfWork;
		private  Mock<IVariationRepository> _mockVariationRepository;
		private  VariationService _variationService;

		[SetUp]
		public void SetUp()
		{
			_mockUnitOfWork = new Mock<IUnitOfWork>();
			_mockVariationRepository = new Mock<IVariationRepository>();
			_mockUnitOfWork.Setup(x => x.Variations).Returns(_mockVariationRepository.Object);
			_variationService = new VariationService(_mockUnitOfWork.Object);
		}
		[Test]
		public void AddVariation_ShouldCallRepository()
		{
			var variation = new Variation();
			_variationService.AddVariation(variation);
			_mockVariationRepository.Verify(r => r.AddVariation(variation), Times.Once);
		}

		[Test]
		public void DeleteVariationById_ShouldCallRepository()
		{
			int variationId = 1;
			_variationService.DeleteVariationById(variationId);
			_mockVariationRepository.Verify(r => r.DeleteVariationById(variationId), Times.Once);
		}

		[Test]
		public void DeleteVariationsByCategoryId_ShouldCallRepository()
		{
			int categoryId = 2;
			_variationService.DeleteVariationsByCategoryId(categoryId);
			_mockVariationRepository.Verify(r => r.DeleteVariationsByCategoryId(categoryId), Times.Once);
		}

		[Test]
		public void EditVariation_ShouldCallRepository()
		{
			int id = 1;
			string name = "Updated Name";
			_variationService.EditVariation(id, name);
			_mockVariationRepository.Verify(r => r.EditVariation(id, name), Times.Once);
		}

		[Test]
		public void GetVariationByCategoryId_ShouldReturnVariations()
		{
			int categoryId = 3;
			var expected = new List<Variation> { new Variation(), new Variation() };
			_mockVariationRepository.Setup(r => r.GetVariationByCategoryId(categoryId)).Returns(expected);

			var result = _variationService.GetVariationByCategoryId(categoryId);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void GetVariationById_ShouldReturnVariation()
		{
			int variationId = 5;
			var expected = new Variation();
			_mockVariationRepository.Setup(r => r.GetVariationById(variationId)).Returns(expected);

			var result = _variationService.GetVariationById(variationId);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void GetVariationByNameAndCategory_ShouldReturnVariation()
		{
			string name = "Color";
			int categoryId = 4;
			var expected = new Variation();
			_mockVariationRepository.Setup(r => r.GetVariationByNameAndCategory(name, categoryId)).Returns(expected);

			var result = _variationService.GetVariationByNameAndCategory(name, categoryId);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void GetVariationOptionsByIds_ShouldReturnOptions()
		{
			var ids = new List<int> { 1, 2 };
			var expected = new List<VariationOption> { new VariationOption(), new VariationOption() };
			_mockVariationRepository.Setup(r => r.GetVariationOptionsByIds(ids)).Returns(expected);

			var result = _variationService.GetVariationOptionsByIds(ids);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void AddListOfVariationsAndThierOptions_ShouldCallRepository()
		{
			var variations = new List<Variation> { new Variation(), new Variation() };
			int categoryId = 1;

			_variationService.AddListOfVariationsAndThierOptions(variations, categoryId);

			_mockVariationRepository.Verify(r => r.AddListOfVariationsAndThierOptions(variations, categoryId), Times.Once);
		}

		[Test]
		public void SaveChange_ShouldCallUnitOfWorkSave()
		{
			_variationService.SaveChange();
			_mockUnitOfWork.Verify(u => u.Save(), Times.Once);
		}
	}
}
