﻿// <auto-generated />
using System;
using BlogsCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogsCore.Migrations
{
    [DbContext(typeof(BloggingContext))]
    [Migration("20180709054757_InitailCreate")]
    partial class InitailCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogsCore.Models.BankAccount", b =>
                {
                    b.Property<int>("BillingDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankName");

                    b.Property<string>("Number");

                    b.Property<string>("Owner");

                    b.Property<string>("Swift");

                    b.HasKey("BillingDetailId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("BlogsCore.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogType");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Url");

                    b.HasKey("BlogId");

                    b.HasIndex("Url")
                        .IsUnique()
                        .HasFilter("[Url] IS NOT NULL");

                    b.ToTable("Blogs");

                    b.HasDiscriminator<int>("BlogType").HasValue(1);
                });

            modelBuilder.Entity("BlogsCore.Models.Car", b =>
                {
                    b.Property<string>("State");

                    b.Property<string>("LicensePlate");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.HasKey("State", "LicensePlate");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("BlogsCore.Models.CreditCard", b =>
                {
                    b.Property<int>("BillingDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardType");

                    b.Property<int>("ExpiryMonth");

                    b.Property<int>("ExpiryYear");

                    b.Property<string>("Number");

                    b.Property<string>("Owner");

                    b.HasKey("BillingDetailId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("BlogsCore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("BlogsCore.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName")
                        .IsConcurrencyToken();

                    b.HasKey("PersonId");

                    b.HasIndex("FirstName", "LastName");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("BlogsCore.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BlogsCore.Models.PostTag", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<string>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("BlogsCore.Models.RecordOfSale", b =>
                {
                    b.Property<int>("RecordOfSaleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarLicensePlate");

                    b.Property<string>("CarState");

                    b.Property<DateTime>("DateSold");

                    b.Property<decimal>("Price");

                    b.HasKey("RecordOfSaleId");

                    b.HasIndex("CarState", "CarLicensePlate");

                    b.ToTable("RecordOfSale");
                });

            modelBuilder.Entity("BlogsCore.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("BlogsCore.Models.RssBlog", b =>
                {
                    b.HasBaseType("BlogsCore.Models.Blog");

                    b.Property<string>("RssUrl");

                    b.ToTable("RssBlog");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("BlogsCore.Models.Order", b =>
                {
                    b.OwnsOne("BlogsCore.Models.StreetAddress", "StreetAddress", b1 =>
                        {
                            b1.Property<int?>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City");

                            b1.Property<string>("Street");

                            b1.ToTable("Order");

                            b1.HasOne("BlogsCore.Models.Order")
                                .WithOne("StreetAddress")
                                .HasForeignKey("BlogsCore.Models.StreetAddress", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("BlogsCore.Models.Post", b =>
                {
                    b.HasOne("BlogsCore.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlogsCore.Models.PostTag", b =>
                {
                    b.HasOne("BlogsCore.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlogsCore.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlogsCore.Models.RecordOfSale", b =>
                {
                    b.HasOne("BlogsCore.Models.Car", "Car")
                        .WithMany("SaleHistory")
                        .HasForeignKey("CarState", "CarLicensePlate");
                });
#pragma warning restore 612, 618
        }
    }
}
