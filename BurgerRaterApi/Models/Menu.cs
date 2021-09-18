using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerRaterApi.Models
{
    [Table("Menu")]
    public class Menu : BaseEntity
    {
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public ICollection<Burger> Burgers { get; set; }
    }
}
