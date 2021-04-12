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
    public class BikeConfiguration : IEntityTypeConfiguration<Bikes>
    {
        public void Configure(EntityTypeBuilder<Bikes> modelBuilder)
        {
            modelBuilder            // PK för cykeln
                .HasKey(x => x.BikeId);

            modelBuilder
                .Property(x => x.BikeId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder
                .Property(x => x.WheelSize)
                .IsRequired(false);

            modelBuilder
                .Property(x => x.Model)
                .IsRequired();

            modelBuilder
                .Property(x => x.PricePerHour)
                .IsRequired();

            modelBuilder            // FK Store ID där cykeln hör hemma
                .HasOne(s => s.Store)
                .WithMany(x => x.Bikes)
                .IsRequired(true);

        }

    }
}
