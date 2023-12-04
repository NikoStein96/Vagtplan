﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vagtplan.Data;

#nullable disable

namespace Vagtplan.Migrations
{
    [DbContext(typeof(ShiftPlannerContext))]
    [Migration("20231203131840_EmployeeIdString")]
    partial class EmployeeIdString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vagtplan.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DayDateDb")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("Vagtplan.Models.Employee", b =>
                {
                    b.Property<string>("FirebaseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pay")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FirebaseId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Vagtplan.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTimeDb")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTimeDb")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Vagtplan.Models.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFisnished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("Vagtplan.Models.Day", b =>
                {
                    b.HasOne("Vagtplan.Models.Schedule", "Schedule")
                        .WithMany("Days")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Vagtplan.Models.Shift", b =>
                {
                    b.HasOne("Vagtplan.Models.Day", "Day")
                        .WithMany("Shifts")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vagtplan.Models.Employee", "Employee")
                        .WithMany("Shifts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Vagtplan.Models.Day", b =>
                {
                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("Vagtplan.Models.Employee", b =>
                {
                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("Vagtplan.Models.Schedule", b =>
                {
                    b.Navigation("Days");
                });
#pragma warning restore 612, 618
        }
    }
}