﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(PuzzleContext))]
    [Migration("20210501191619_WDP-Init")]
    partial class WDPInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("BL.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PuzzleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UIdC")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PuzzleId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BL.Puzzle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dividend")
                        .HasColumnType("TEXT");

                    b.Property<string>("Divisor")
                        .HasColumnType("TEXT");

                    b.Property<string>("Letters")
                        .HasColumnType("TEXT");

                    b.Property<string>("Quotient")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Seed")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Solved")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SolvedBy")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Puzzles");
                });

            modelBuilder.Entity("BL.Comment", b =>
                {
                    b.HasOne("BL.Puzzle", "Puzzle")
                        .WithMany("Comments")
                        .HasForeignKey("PuzzleId");

                    b.Navigation("Puzzle");
                });

            modelBuilder.Entity("BL.Puzzle", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
