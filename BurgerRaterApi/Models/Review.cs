using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerRaterApi.Models
{
    [Table("Review")]
    public class Review : BaseEntity
    {
        [Required]
        public int TasteScore { get; set; }

        [Required]
        public int TextureScore { get; set; }

        [Required]
        public int VisualPresentationScore { get; set; }

        public byte[] Image { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
