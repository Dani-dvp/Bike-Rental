using BikeRental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.OrderId);

            modelBuilder
                .Property(x => x.OrderId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder
                .HasOne(x => x.Customer)
                .WithMany(o => o.Orders);

            modelBuilder
                .HasMany(x => x.Bikes)
                .WithMany(b => b.Orders);

            modelBuilder
                .Property(x => x.Date)
                .IsRequired();

            modelBuilder
                .Property(x => x.HoursRented)
                .IsRequired();
        }
    }
}

