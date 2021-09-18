using BurgerRaterApi.Models.Dto.Burger;
using System.Collections.Generic;

namespace BurgerRaterApi.Models.Dto.Menu
{
    public class MenuCreateDto
    {
        public IEnumerable<BurgerCreateDto> Burgers { get; set; }
    }
}
