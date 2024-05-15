﻿// <auto-generated />
using System;
using Central1zedCSharp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Central1zedCSharp.Data.Migrations
{
    [DbContext(typeof(EndpointContext))]
    [Migration("20240515152817_InitialCreation3")]
    partial class InitialCreation3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Central1zedCSharp.Entities.Confirmation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ClientEndpointConfirmations");
                });

            modelBuilder.Entity("Central1zedCSharp.Entities.EndpointClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfirmationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TokenId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ConfirmationId");

                    b.HasIndex("TokenId");

                    b.ToTable("ClientEndpoints");
                });

            modelBuilder.Entity("Central1zedCSharp.Entities.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EndpointClientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EndpointClientId");

                    b.ToTable("ClientEndpointLogs");
                });

            modelBuilder.Entity("Central1zedCSharp.Entities.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ClientTokens");
                });

            modelBuilder.Entity("Central1zedCSharp.Entities.EndpointClient", b =>
                {
                    b.HasOne("Central1zedCSharp.Entities.Confirmation", "Confirmation")
                        .WithMany()
                        .HasForeignKey("ConfirmationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Central1zedCSharp.Entities.Token", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Confirmation");

                    b.Navigation("Token");
                });

            modelBuilder.Entity("Central1zedCSharp.Entities.Log", b =>
                {
                    b.HasOne("Central1zedCSharp.Entities.EndpointClient", "EndpointClient")
                        .WithMany("Logs")
                        .HasForeignKey("EndpointClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndpointClient");
                });

            modelBuilder.Entity("Central1zedCSharp.Entities.EndpointClient", b =>
                {
                    b.Navigation("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}