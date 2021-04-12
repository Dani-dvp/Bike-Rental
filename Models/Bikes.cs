using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Bikes
    {
        public int BikeId { get; set; }
        public int? WheelSize { get; set; }
        public string Model { get; set; }
        public int PricePerHour { get; set; }
        public ICollection<Order>? Orders { get; set; }

        [ForeignKey("StoreID")]
        public int StoreID { get; set; }
        public Store Store { get; set; }
        public IList<BikesOrder> BikeOrders { get; set; }
    }
}
