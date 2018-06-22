﻿// <auto-generated />
using Bondora.Context;
using Bondora.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Bondora.Migrations
{
    [DbContext(typeof(BondoraContext))]
    partial class BondoraContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bondora.Entities.Equipment", b =>
                {
                    b.Property<Guid>("EquipmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BonusRate");

                    b.Property<decimal>("OneTimeFee");

                    b.Property<int>("PremiumDays");

                    b.Property<decimal>("PremiumFee");

                    b.Property<decimal>("RegularFee");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("EquipmentId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("Bondora.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Bondora.Entities.Rental", b =>
                {
                    b.Property<Guid>("RentalId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Days");

                    b.Property<Guid>("EquipmentId");

                    b.Property<Guid>("OrderId");

                    b.HasKey("RentalId");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("OrderId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("Bondora.Entities.Rental", b =>
                {
                    b.HasOne("Bondora.Entities.Equipment", "Equipment")
                        .WithMany("Rentals")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bondora.Entities.Order", "Order")
                        .WithMany("Rentals")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
