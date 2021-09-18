using BurgerRaterApi.Models.Dto.Menu;

namespace BurgerRaterApi.Models.Dto.Restaurant
{
    public class RestaurantCreateDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string District { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string OpeningTime { get; set; }

        public string ClosingTime { get; set; }

        public MenuCreateDto Menu { get; set; }
    }
}
