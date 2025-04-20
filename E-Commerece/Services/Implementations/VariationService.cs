using E_Commerece.Models;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Interfaces;

namespace E_Commerece.Services.Implementations
{
    public class VariationService : IVariationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VariationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void AddListOfVariationsAndThierOptions(List<Variation> variations, int categoryId)
        {
            this._unitOfWork.Variations.AddListOfVariationsAndThierOptions(variations, categoryId);
        }

        public void AddVariation(Variation variation)
        {
            this._unitOfWork.Variations.AddVariation(variation);
        }

        public void DeleteVariationById(int variationId)
        {
            this._unitOfWork.Variations.DeleteVariationById(variationId);
        }

        public void DeleteVariationsByCategoryId(int categoryId)
        {
            this._unitOfWork.Variations.DeleteVariationsByCategoryId(categoryId);
        }

        public void EditVariation(int id, string name)
        {
            this._unitOfWork.Variations.EditVariation(id, name);
        }

        public List<Variation> GetVariationByCategoryId(int categoryId) => this._unitOfWork.Variations.GetVariationByCategoryId(categoryId);

        public Variation GetVariationById(int variationId) => this._unitOfWork.Variations.GetVariationById(variationId);

        public Variation GetVariationByNameAndCategory(string name, int categoryId) => this._unitOfWork.Variations.GetVariationByNameAndCategory(name, categoryId);

        public List<VariationOption> GetVariationOptionsByIds(List<int> variationOptionIds) => this._unitOfWork.Variations.GetVariationOptionsByIds(variationOptionIds);
        public void SaveChange()
        {
            this._unitOfWork.Save();
        }
    }
}
