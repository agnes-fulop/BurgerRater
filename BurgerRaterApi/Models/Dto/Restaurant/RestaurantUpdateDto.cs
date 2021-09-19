using System;

namespace BurgerRaterApi.Models.Dto.Restaurant
{
    public class RestaurantUpdateDto : RestaurantCreateDto
    {
        public Guid Id { get; set; }
    }
}
