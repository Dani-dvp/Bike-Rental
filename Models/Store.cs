using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }
        public string Location { get; set; }
        public ICollection<Bikes> Bikes { get; set; }
    }
}
