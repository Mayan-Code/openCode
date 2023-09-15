﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(WeightContext))]
    [Migration("20230601200158_AdedTemplateMeal")]
    partial class AdedTemplateMeal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Domain.Entities.Meal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ChangeDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Domain.Entities.Plate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ChangeDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Plates");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Barcode")
                        .HasMaxLength(24)
                        .HasColumnType("varchar(24)");

                    b.Property<decimal>("Carbo")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("ChangeDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Fat")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("FoodCategory")
                        .HasColumnType("int");

                    b.Property<int>("FoodUnit")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Protein")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.ProductMeal", b =>
                {
                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long>("MealId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ChangeDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("ProductId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("ProductMeals");
                });

            modelBuilder.Entity("Domain.Entities.TemplateMeal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ChangeDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("TemplateMeals");
                });

            modelBuilder.Entity("Domain.Entities.TemplateMealProduct", b =>
                {
                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long>("TemplateMealId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ChangeDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("ProductId", "TemplateMealId");

                    b.HasIndex("TemplateMealId");

                    b.ToTable("TemplateMealProducts");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ChangeDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.EntityFrameworkCore.AutoHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Changed")
                        .HasMaxLength(2048)
                        .HasColumnType("varchar(2048)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<string>("RowId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("AutoHistories");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AutoHistory");
                });

            modelBuilder.Entity("Infrastructure.Data.AutoHistoryExt.CustomAutoHistory", b =>
                {
                    b.HasBaseType("Microsoft.EntityFrameworkCore.AutoHistory");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("CustomAutoHistory");
                });

            modelBuilder.Entity("Domain.Entities.Meal", b =>
                {
                    b.HasOne("Domain.Entities.User", "Creator")
                        .WithMany("Meals")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Domain.Entities.Plate", b =>
                {
                    b.HasOne("Domain.Entities.User", "Creator")
                        .WithMany("Plates")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.User", "Creator")
                        .WithMany("Products")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Domain.Entities.ProductMeal", b =>
                {
                    b.HasOne("Domain.Entities.Meal", "Meal")
                        .WithMany("Products")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("Meals")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.TemplateMeal", b =>
                {
                    b.HasOne("Domain.Entities.User", "Creator")
                        .WithMany("TemplateMeals")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Domain.Entities.TemplateMealProduct", b =>
                {
                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("TemplateMeals")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.TemplateMeal", "TemplateMeal")
                        .WithMany("Products")
                        .HasForeignKey("TemplateMealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("TemplateMeal");
                });

            modelBuilder.Entity("Domain.Entities.Meal", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("Meals");

                    b.Navigation("TemplateMeals");
                });

            modelBuilder.Entity("Domain.Entities.TemplateMeal", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Meals");

                    b.Navigation("Plates");

                    b.Navigation("Products");

                    b.Navigation("TemplateMeals");
                });
#pragma warning restore 612, 618
        }
    }
}
