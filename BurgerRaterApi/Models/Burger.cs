using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerRaterApi.Models
{
    [Table("Burger")]
    public class Burger : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Burger name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Ingresdients list cannot be longer than 500 characters.")]
        public string Ingredients { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Currency { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
