using BikeRental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeRental.EntityConfigurations
{
    public class BikesOrderConfiguration : IEntityTypeConfiguration<BikesOrder>
    {
        public void Configure(EntityTypeBuilder<BikesOrder> modelBuilder)
        {
            modelBuilder
                .HasKey(b => new { b.BikeId, b.OrderId });


        }
    }
}
