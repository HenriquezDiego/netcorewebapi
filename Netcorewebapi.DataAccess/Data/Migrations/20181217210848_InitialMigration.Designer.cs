﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Netcorewebapi.DataAccess.Persistence;

namespace Netcorewebapi.DataAccess.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181217210848_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Netcorewebapi.DataAccess.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OrderNumber");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Netcorewebapi.DataAccess.Data.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18, 5)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Netcorewebapi.DataAccess.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArtDating");

                    b.Property<string>("ArtDescription");

                    b.Property<string>("ArtId");

                    b.Property<string>("Artist");

                    b.Property<DateTime>("ArtistBirthDate");

                    b.Property<DateTime>("ArtistDeathDate");

                    b.Property<string>("ArtistNationality");

                    b.Property<string>("Category");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 5)");

                    b.Property<string>("Size");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Netcorewebapi.DataAccess.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("Netcorewebapi.DataAccess.Data.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("Netcorewebapi.DataAccess.Data.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
