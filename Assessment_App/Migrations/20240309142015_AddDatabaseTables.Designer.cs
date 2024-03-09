﻿// <auto-generated />
using System;
using Assessment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assessment_App.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240309142015_AddDatabaseTables")]
    partial class AddDatabaseTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Assessment_App.Models.Border", b =>
                {
                    b.Property<int>("BorderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BorderId"));

                    b.Property<string>("BorderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.HasKey("BorderId");

                    b.HasIndex("CountryId");

                    b.ToTable("Border");
                });

            modelBuilder.Entity("Assessment_App.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("Capital")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Assessment_App.Models.Border", b =>
                {
                    b.HasOne("Assessment_App.Models.Country", null)
                        .WithMany("Borders")
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("Assessment_App.Models.Country", b =>
                {
                    b.Navigation("Borders");
                });
#pragma warning restore 612, 618
        }
    }
}
