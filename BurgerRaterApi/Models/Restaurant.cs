using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerRaterApi.Models
{
    [Table("Restaurant")]
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string District { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string OpeningTime { get; set; }

        public string ClosingTime { get; set; }
    }
}
