using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerRaterApi.Models
{
    [Table("Restaurant")]
    public class Restaurant : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Restaurant name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Address cannot be longer than 15 characters.")]
        public string District { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Zip code cannot be longer than 15 characters.")]
        public string ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        public string OpeningTime { get; set; }

        public string ClosingTime { get; set; }

        public virtual ICollection<Burger> Burgers { get; set; } = new List<Burger>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
