﻿// <auto-generated />
using System;
using MeterApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeterApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231208062346_Initialize_Database")]
    partial class Initialize_Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MeterApp.Models.Error", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StallCode")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("StallCode");

                    b.HasIndex("Timestamp");

                    b.ToTable("Errors", (string)null);
                });

            modelBuilder.Entity("MeterApp.Models.GasMeter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Cycle")
                        .HasColumnType("integer");

                    b.Property<string>("Error")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FromTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Pre")
                        .HasMaxLength(40)
                        .HasColumnType("double precision");

                    b.Property<string>("Rate")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<double>("Raw")
                        .HasMaxLength(40)
                        .HasColumnType("double precision");

                    b.Property<string>("StallCode")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<DateTime>("ToTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Value")
                        .HasMaxLength(40)
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Cycle");

                    b.HasIndex("FromTimestamp");

                    b.HasIndex("StallCode");

                    b.HasIndex("ToTimestamp");

                    b.HasIndex("Value");

                    b.ToTable("GasMeters", (string)null);
                });

            modelBuilder.Entity("MeterApp.Models.Stall", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double?>("LastGasMeter")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("LastGasMeterDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LastGasMeterId")
                        .HasColumnType("uuid");

                    b.Property<double?>("LastWaterMeter")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("LastWaterMeterDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LastWaterMeterId")
                        .HasColumnType("uuid");

                    b.Property<double?>("LatestGasMeter")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("LatestGasMeterDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LatestGasMeterId")
                        .HasColumnType("uuid");

                    b.Property<double?>("LatestWaterMeter")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("LatestWaterMeterDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LatestWaterMeterId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("StallCode")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<bool>("UseGasMeter")
                        .HasColumnType("boolean");

                    b.Property<bool>("UseWaterMeter")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("StallCode");

                    b.ToTable("Stalls", (string)null);
                });

            modelBuilder.Entity("MeterApp.Models.WaterMeter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Cycle")
                        .HasColumnType("integer");

                    b.Property<string>("Error")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FromTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Pre")
                        .HasMaxLength(40)
                        .HasColumnType("double precision");

                    b.Property<string>("Rate")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<double>("Raw")
                        .HasMaxLength(40)
                        .HasColumnType("double precision");

                    b.Property<string>("StallCode")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<DateTime>("ToTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Value")
                        .HasMaxLength(40)
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Cycle");

                    b.HasIndex("FromTimestamp");

                    b.HasIndex("StallCode");

                    b.HasIndex("ToTimestamp");

                    b.HasIndex("Value");

                    b.ToTable("WaterMeters", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
