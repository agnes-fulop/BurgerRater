using System;

namespace BurgerRaterApi.Models.Dto.Review
{
    public class ReviewResponseDto : ReviewCreateDto
    {
        public Guid Id { get; set; }
    }
}
