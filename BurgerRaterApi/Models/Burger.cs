﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerRaterApi.Models
{
    [Table("Burger")]
    public class Burger : BaseEntity
    {
        public string Name { get; set; }

        public string Ingredients { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
