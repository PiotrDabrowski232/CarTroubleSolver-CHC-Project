

using TheCarMarket.Data.Models.Enums;

namespace TheCarMarket.Data.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public CarBrand Brand { get; set; }
        public ICollection<CarModel> CarModels { get; set; }


    }
}
