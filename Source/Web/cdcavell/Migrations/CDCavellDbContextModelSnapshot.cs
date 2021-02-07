﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cdcavell.Data;

namespace cdcavell.Migrations
{
    [DbContext(typeof(CDCavellDbContext))]
    partial class CDCavellDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("cdcavell.Data.AuditHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Application")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("Entity")
                        .HasColumnType("TEXT");

                    b.Property<string>("KeyValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalValues")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AuditHistory");
                });

            modelBuilder.Entity("cdcavell.Data.SiteMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Controller")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastSubmitDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SiteMap");
                });
#pragma warning restore 612, 618
        }
    }
}
