using System;

namespace BurgerRaterApi.Models.Dto.Burger
{
    public class BurgerResponseDto : BurgerCreateDto
    {
        public Guid Id { get; set; }
    }
}
