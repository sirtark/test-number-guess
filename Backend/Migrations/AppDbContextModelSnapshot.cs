﻿// <auto-generated />
using Backend;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("Backend.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("CurrentTries")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FK_Session")
                        .HasColumnType("TEXT");

                    b.Property<byte>("Number")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FK_Session");

                    b.ToTable("AliveGames", (string)null);
                });

            modelBuilder.Entity("Backend.Session", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Backend.Game", b =>
                {
                    b.HasOne("Backend.Session", "Session")
                        .WithMany()
                        .HasForeignKey("FK_Session");

                    b.Navigation("Session");
                });
#pragma warning restore 612, 618
        }
    }
}
