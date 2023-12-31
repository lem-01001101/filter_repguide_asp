﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Unicel_init2.Data;

#nullable disable

namespace Unicel_init2.Migrations
{
    [DbContext(typeof(UnicelDbContext))]
    [Migration("20231205034123_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FiltersOEM", b =>
                {
                    b.Property<Guid>("FiltersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OEMId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FiltersId", "OEMId");

                    b.HasIndex("OEMId");

                    b.ToTable("FiltersOEM");
                });

            modelBuilder.Entity("Unicel_init2.Models.Domain.Filters", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BottomEndCap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Length")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Media")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PleatCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopEndCap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Filters");
                });

            modelBuilder.Entity("Unicel_init2.Models.Domain.OEM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OEM");
                });

            modelBuilder.Entity("FiltersOEM", b =>
                {
                    b.HasOne("Unicel_init2.Models.Domain.Filters", null)
                        .WithMany()
                        .HasForeignKey("FiltersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Unicel_init2.Models.Domain.OEM", null)
                        .WithMany()
                        .HasForeignKey("OEMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
