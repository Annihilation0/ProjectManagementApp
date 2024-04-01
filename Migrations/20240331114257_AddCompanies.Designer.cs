﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagementApp.Data;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240331114257_AddCompanies")]
    partial class AddCompanies
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("ProjectManagementApp.Models.Models.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .HasColumnType("TEXT");

                    b.HasKey("CompanyID");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            CompanyID = 1,
                            CompanyName = "Company A"
                        },
                        new
                        {
                            CompanyID = 2,
                            CompanyName = "Company B"
                        },
                        new
                        {
                            CompanyID = 3,
                            CompanyName = "Company C"
                        },
                        new
                        {
                            CompanyID = 4,
                            CompanyName = "Company D"
                        },
                        new
                        {
                            CompanyID = 5,
                            CompanyName = "Company E"
                        });
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeeID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientCompanyID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int>("ExecutionCompanyID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Priority")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectManagerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProjectName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("ProjectID");

                    b.HasIndex("ClientCompanyID");

                    b.HasIndex("ExecutionCompanyID");

                    b.HasIndex("ProjectManagerID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Models.ProjectEmployee", b =>
                {
                    b.Property<int>("ProjectEmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.HasKey("ProjectEmployeeID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("ProjectID");

                    b.ToTable("ProjectEmployees");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Models.Employee", b =>
                {
                    b.HasOne("ProjectManagementApp.Models.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Models.Project", b =>
                {
                    b.HasOne("ProjectManagementApp.Models.Models.Company", "ClientCompany")
                        .WithMany()
                        .HasForeignKey("ClientCompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementApp.Models.Models.Company", "ExecutionCompany")
                        .WithMany()
                        .HasForeignKey("ExecutionCompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementApp.Models.Models.Employee", "ProjectManager")
                        .WithMany()
                        .HasForeignKey("ProjectManagerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCompany");

                    b.Navigation("ExecutionCompany");

                    b.Navigation("ProjectManager");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Models.ProjectEmployee", b =>
                {
                    b.HasOne("ProjectManagementApp.Models.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementApp.Models.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Models.Company", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
