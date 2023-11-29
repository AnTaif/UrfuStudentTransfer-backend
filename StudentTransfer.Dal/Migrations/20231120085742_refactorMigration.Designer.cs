﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StudentTransfer.Dal;

#nullable disable

namespace StudentTransfer.Dal.Migrations
{
    [DbContext(typeof(StudentTransferContext))]
    [Migration("20231120085742_refactorMigration")]
    partial class refactorMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Application.ApplicationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentStatus")
                        .HasColumnType("integer");

                    b.Property<int>("DirectionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DirectionId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Application.ApplicationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApplicationEntityId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationEntityId");

                    b.ToTable("ApplicationStatus");
                });

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Application.Direction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Course")
                        .HasColumnType("integer");

                    b.Property<int>("Form")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Direction");
                });

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Application.FileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("ApplicationEntityId")
                        .HasColumnType("integer");

                    b.Property<string>("Extension")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationEntityId");

                    b.ToTable("FileEntity");
                });

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Vacant.VacantDirection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Contracts")
                        .HasColumnType("integer");

                    b.Property<int>("Course")
                        .HasColumnType("integer");

                    b.Property<int>("FederalBudgets")
                        .HasColumnType("integer");

                    b.Property<int>("Form")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("LocalBudgets")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SubjectsBudgets")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("VacantList");
                });

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Application.ApplicationEntity", b =>
                {
                    b.HasOne("StudentTransfer.Dal.Entities.Application.Direction", "Direction")
                        .WithMany()
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direction");
                });

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Application.ApplicationStatus", b =>
                {
                    b.HasOne("StudentTransfer.Dal.Entities.Application.ApplicationEntity", null)
                        .WithMany("Updates")
                        .HasForeignKey("ApplicationEntityId");
                });

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Application.FileEntity", b =>
                {
                    b.HasOne("StudentTransfer.Dal.Entities.Application.ApplicationEntity", null)
                        .WithMany("Files")
                        .HasForeignKey("ApplicationEntityId");
                });

            modelBuilder.Entity("StudentTransfer.Dal.Entities.Application.ApplicationEntity", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Updates");
                });
#pragma warning restore 612, 618
        }
    }
}