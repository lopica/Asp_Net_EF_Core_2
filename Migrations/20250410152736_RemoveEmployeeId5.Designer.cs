﻿// <auto-generated />
using System;
using Asp_Net_EF_Core_1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Asp_Net_EF_Core_1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250410152736_RemoveEmployeeId5")]
    partial class RemoveEmployeeId5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-aaaa-bbbb-cccc-111111111111"),
                            Name = "Software Development"
                        },
                        new
                        {
                            Id = new Guid("22222222-bbbb-cccc-dddd-222222222222"),
                            Name = "Finance"
                        },
                        new
                        {
                            Id = new Guid("33333333-cccc-dddd-eeee-333333333333"),
                            Name = "Accountant"
                        },
                        new
                        {
                            Id = new Guid("44444444-dddd-eeee-ffff-444444444444"),
                            Name = "HR"
                        });
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("JoinedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("SalaryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SalaryId")
                        .IsUnique()
                        .HasFilter("[SalaryId] IS NOT NULL");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.ProjectEmployee", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ProjectEmployees");
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.Salary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.Employee", b =>
                {
                    b.HasOne("Asp_Net_EF_Core_1.Domains.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Asp_Net_EF_Core_1.Domains.Salary", "Salary")
                        .WithOne("Employee")
                        .HasForeignKey("Asp_Net_EF_Core_1.Domains.Employee", "SalaryId");

                    b.Navigation("Department");

                    b.Navigation("Salary");
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.ProjectEmployee", b =>
                {
                    b.HasOne("Asp_Net_EF_Core_1.Domains.Employee", "Employee")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asp_Net_EF_Core_1.Domains.Project", "Project")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.Employee", b =>
                {
                    b.Navigation("ProjectEmployees");
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.Project", b =>
                {
                    b.Navigation("ProjectEmployees");
                });

            modelBuilder.Entity("Asp_Net_EF_Core_1.Domains.Salary", b =>
                {
                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
