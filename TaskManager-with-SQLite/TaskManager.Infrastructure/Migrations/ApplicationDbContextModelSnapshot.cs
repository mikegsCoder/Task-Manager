﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.Infrastructure.Data;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = "669fe84c-19ce-41f9-ba64-2c844d2b851b",
                            Name = "Personal"
                        },
                        new
                        {
                            Id = "9c24c7ac-b86f-4a30-a0fd-bb3ef6e76308",
                            Name = "Job"
                        },
                        new
                        {
                            Id = "4a9861e0-7884-4d4c-9080-b48de9c883ac",
                            Name = "Family"
                        },
                        new
                        {
                            Id = "43e674b8-ae0e-48a0-a231-e1bc5cf85e9c",
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.Remark", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserTaskId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserTaskId");

                    b.ToTable("Remarks");

                    b.HasData(
                        new
                        {
                            Id = "971138d8-d689-4820-8f23-535d1bac1ade",
                            Content = "Create application database with at least 5 database models.",
                            CreatedOn = new DateTime(2025, 3, 20, 7, 16, 59, 577, DateTimeKind.Local).AddTicks(7836),
                            IsDeleted = false,
                            IsFinished = false,
                            UserTaskId = "23bf9c00-056c-4c72-9fa7-396c28da66c7"
                        },
                        new
                        {
                            Id = "6eb3a493-8160-475f-a4f3-62089765c162",
                            Content = "Use separate projects for database, business logic and WPF application.",
                            CreatedOn = new DateTime(2025, 3, 20, 7, 16, 59, 577, DateTimeKind.Local).AddTicks(7877),
                            IsDeleted = false,
                            IsFinished = false,
                            UserTaskId = "23bf9c00-056c-4c72-9fa7-396c28da66c7"
                        });
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.Status", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = "f83e8ace-ea2c-48f0-846d-1f86c7eb127d",
                            Name = "Awaiting"
                        },
                        new
                        {
                            Id = "0a7bead2-d075-4100-b803-05498f07347b",
                            Name = "In Progress"
                        },
                        new
                        {
                            Id = "2b381501-15b1-488b-96e2-950869d68d79",
                            Name = "Finished"
                        },
                        new
                        {
                            Id = "d920dc1b-fab5-40cc-a387-81f7059da658",
                            Name = "Canceled"
                        });
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("LatName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "90b21bc9-9062-4142-b3f9-774e6797e080",
                            FirstName = "Peter",
                            LatName = "Petrov",
                            PasswordHash = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413",
                            Username = "peter123"
                        });
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.UserTask", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("StatusId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = "23bf9c00-056c-4c72-9fa7-396c28da66c7",
                            CategoryId = "669fe84c-19ce-41f9-ba64-2c844d2b851b",
                            CreatedOn = new DateTime(2025, 3, 20, 7, 16, 59, 577, DateTimeKind.Local).AddTicks(7937),
                            Description = "Create final project for the WPF course.",
                            IsDeleted = false,
                            IsFinished = false,
                            StatusId = "0a7bead2-d075-4100-b803-05498f07347b",
                            UserId = "90b21bc9-9062-4142-b3f9-774e6797e080"
                        });
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.Remark", b =>
                {
                    b.HasOne("TaskManager.Infrastructure.Data.Models.DataBaseModels.UserTask", "Task")
                        .WithMany("Remarks")
                        .HasForeignKey("UserTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.UserTask", b =>
                {
                    b.HasOne("TaskManager.Infrastructure.Data.Models.DataBaseModels.Category", "Category")
                        .WithMany("Tasks")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Infrastructure.Data.Models.DataBaseModels.Status", "Status")
                        .WithMany("Tasks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Infrastructure.Data.Models.DataBaseModels.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.Category", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.Status", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.User", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManager.Infrastructure.Data.Models.DataBaseModels.UserTask", b =>
                {
                    b.Navigation("Remarks");
                });
#pragma warning restore 612, 618
        }
    }
}
