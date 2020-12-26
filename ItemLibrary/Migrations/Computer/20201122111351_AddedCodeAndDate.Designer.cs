﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModelLibrary.DataContexts;

namespace ModelLibrary.Migrations
{
    [DbContext(typeof(ComputerContext))]
    [Migration("20201122111351_AddedCodeAndDate")]
    partial class AddedCodeAndDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ItemLibrary.Computer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ComputerCategory")
                        .HasColumnType("int");

                    b.Property<string>("GraphicsCardMemory")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("GraphicsCardName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ImageLink")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("ItemCategory")
                        .HasColumnType("int");

                    b.Property<int>("ItemCode")
                        .HasColumnType("int");

                    b.Property<string>("ItemURL")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("ManufacturerName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("ProcessorId")
                        .HasColumnType("int");

                    b.Property<int>("RAM")
                        .HasColumnType("int");

                    b.Property<string>("RAM_type")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Resolution")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("ShopName")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("StorageCapacity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProcessorId");

                    b.ToTable("Computers");
                });

            modelBuilder.Entity("ItemLibrary.Processor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Cache")
                        .HasColumnType("int");

                    b.Property<int>("MinCores")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Processors");
                });

            modelBuilder.Entity("ItemLibrary.Computer", b =>
                {
                    b.HasOne("ItemLibrary.Processor", "Processor")
                        .WithMany()
                        .HasForeignKey("ProcessorId");

                    b.Navigation("Processor");
                });
#pragma warning restore 612, 618
        }
    }
}
