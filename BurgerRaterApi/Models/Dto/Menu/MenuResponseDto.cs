using BurgerRaterApi.Models.Dto.Burger;
using System.Collections.Generic;

namespace BurgerRaterApi.Models.Dto.Menu
{
    public class MenuResponseDto
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public IEnumerable<BurgerResponseDto> Burgers { get; set; }
    }
}
