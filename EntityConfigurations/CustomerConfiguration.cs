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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.CustomerId);

            modelBuilder
                .Property(x => x.CustomerId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder
                .Property(x => x.PhoneNumber)
                .IsRequired();

            modelBuilder
                .Property(x => x.FirstName)
                .IsRequired(false);

            modelBuilder
                .Property(x => x.LastName)
                .IsRequired();

            modelBuilder
                .Property(x => x.DateOfBirth)
                .IsRequired();

            modelBuilder
                .HasMany(o => o.Orders)
                .WithOne(c => c.Customer);

        }

    }
}
