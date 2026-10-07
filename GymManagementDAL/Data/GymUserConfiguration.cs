using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Data
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.PhoneNumber)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUserValidEmailCheck", "Email Like '_%@_%._%'");
                tb.HasCheckConstraint("GymUserValidPhoneNumber", "PhoneNumber Like '01%' and PhoneNumber Not Like '%[^0-9]%'");
            });

            builder.HasIndex(x => x.Email).IsUnique();  
            builder.HasIndex(x => x.PhoneNumber).IsUnique();

            builder.OwnsOne(x => x.Address, AddressBuilder =>
            {
                AddressBuilder.Property(x => x.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddressBuilder.Property(x => x.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddressBuilder.Property(x => x.BuildingNumber)
                .HasColumnName("BuildingNumber")
                .HasColumnType("varchar")
                .HasMaxLength(30);

            }); 
        }
    }
}
