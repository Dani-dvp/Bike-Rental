using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Bikes> Bikes { get; set; }
        public int Date { get; set; }
        public int HoursRented { get; set; }

        [ForeignKey("CustomerId")]
        public int customerId { get; set; }

        public IList<BikesOrder> BikeOrders { get; set; }
    }
}
