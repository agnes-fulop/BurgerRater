using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BurgerRaterApi.Models.Dto.Review
{
    public class ReviewCreateDto
    {
        [Required]
        public int TasteScore { get; set; }

        [Required]
        public int TextureScore { get; set; }

        [Required]
        public int VisualPresentationScore { get; set; }

        public IFormFile Image { get; set; }
    }
}
