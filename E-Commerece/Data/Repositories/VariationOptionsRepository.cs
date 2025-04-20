using E_Commerece.Data.Interfaces;
using E_Commerece.Models;

namespace E_Commerece.Data.Repositories
{
    public class VariationOptionsRepository : IVariationOptionsRepository
    {
        private readonly EcommereceContext _context;
        public VariationOptionsRepository(EcommereceContext context)
        {
            this._context = context;
        }

        public void CreateVariationOption(VariationOption variationOption)
        {
            this._context.VariationOptions.Add(variationOption);
        }

        public void DeleteVariationOption(VariationOption variationOption)
        {
            this._context.VariationOptions.Remove(variationOption);
        }

        public void DeleteVariationOptionsByVariationId(int variationid)
        {
            var variationoptions = GetVariationOptionsByVariationId(variationid);
            this._context.VariationOptions.RemoveRange(variationoptions);
        }

        public void EditVariationOption(string value, int optionid)
        {
            var variationoption = GetVariationOptionById(optionid);
            variationoption.Value = value;
        }

        public VariationOption GetByValueAndVariation(string value, int variationId)
        {
            return _context.VariationOptions
                .FirstOrDefault(vo => vo.Value.ToLower() == value.ToLower() && vo.VariationId == variationId);
        }


        public VariationOption GetVariationOptionById(int variationoptionid) => this._context.VariationOptions.Where(x => x.Id == variationoptionid).FirstOrDefault();

        public List<VariationOption> GetVariationOptionsByVariationId(int variationid) => this._context.VariationOptions.Where(x => x.VariationId == variationid).ToList();
    }
}
