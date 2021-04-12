using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeRental.Models
{
    public class BikesOrder
    {
        public int BikeId { get; set; }
        public Bikes bike { get; set; }
        public int OrderId { get; set; }
        public Order order { get; set; }
    }
}
