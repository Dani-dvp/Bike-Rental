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
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.StoreID);

            modelBuilder
                .Property(x => x.StoreID)
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder
                .Property(x => x.Location)
                .IsRequired();

            modelBuilder
                .HasMany(x => x.Bikes)
                .WithOne(x => x.Store)
                .IsRequired(false);

                
        }
    }
}
