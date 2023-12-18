﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cemerenbwebapi.Data;

#nullable disable

namespace cemerenbwebapi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Cart.Cart", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("MenuItemId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("StoreEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Carts");
            });

            modelBuilder.Entity("Models.Menu.Menu", b =>
            {
                b.Property<int>("MenuId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"));

                b.Property<int>("MenuItemCategory")
                    .HasColumnType("int");

                b.Property<string>("MenuItemDescription")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("MenuItemId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("MenuItemImageLink")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("MenuItemIsAvaliable")
                    .HasColumnType("int");

                b.Property<string>("MenuItemName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<float>("MenuItemPrice")
                    .HasColumnType("real");

                b.Property<string>("StoreEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("MenuId");

                b.ToTable("Menus");
            });

            modelBuilder.Entity("Models.Order.Order", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("OrderCreatingTime")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("OrderId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("OrderNote")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("OrderStatus")
                    .HasColumnType("int");

                b.Property<double>("OrderTotalPrice")
                    .HasColumnType("double");

                b.Property<string>("StoreEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");
                b.Property<int>("ItemCount")
                   .HasColumnType("int");

                b.Property<string>("UserEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Orders");
            });
            


            modelBuilder.Entity("Models.Rating.Rating", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Comment")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("IsRatingDisplayed")
                    .HasColumnType("int");

                b.Property<string>("OrderId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("RatingDate")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("RatingDisabledComment")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("RatingId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("RatingPoint")
                    .HasColumnType("int");

                b.Property<string>("StoreEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

               

                b.Property<string>("UserEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                

                b.HasKey("Id");

                b.ToTable("Ratings");
            });

            modelBuilder.Entity("Models.PointRule.PointRule", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("IsPointEnabled")
                    .IsRequired()
                    .HasColumnType("int");

                
                b.Property<string>("StoreEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("PointsToGain")
                    .IsRequired()
                    .HasColumnType("int");
                b.Property<int>("Category1Gain")
                   .IsRequired()
                   .HasColumnType("int");
                b.Property<int>("Category2Gain")
                   .IsRequired()
                   .HasColumnType("int");
                b.Property<int>("Category3Gain")
                   .IsRequired()
                   .HasColumnType("int");
                b.Property<int>("Category4Gain")
                   .IsRequired()
                   .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("PointRules");
            });

            modelBuilder.Entity("Models.Point.Point", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
                b.Property<string>("UserEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("StoreEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("TotalPoint")
                    .IsRequired()
                    .HasColumnType("int");
                b.Property<int>("TotalGained")
                   .IsRequired()
                   .HasColumnType("int");
               

                b.HasKey("Id");

                b.ToTable("Points");
            });

            modelBuilder.Entity("Models.Store.Store", b =>
            {
                b.Property<int>("StoreId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                b.Property<string>("StoreClosingTime")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("StoreCoverImageLink")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("StoreEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("StoreIsOn")
                    .HasColumnType("int");

                b.Property<string>("StoreLogoLink")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("StoreName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("StoreOpeningTime")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<byte[]>("StorePasswordHash")
                    .IsRequired()
                    .HasColumnType("varbinary(max)");

                b.Property<string>("StorePasswordResetToken")
                    .HasColumnType("nvarchar(max)");

                b.Property<byte[]>("StorePasswordSalt")
                    .IsRequired()
                    .HasColumnType("varbinary(max)");

                b.Property<DateTime?>("StoreResetTokenExpires")
                    .HasColumnType("datetime2");

                b.Property<string>("StoreTaxId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("StoreId");

                b.ToTable("Stores");
            });

            modelBuilder.Entity("Models.User.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("FullName")
                    .HasColumnType("nvarchar(max)");

                b.Property<byte[]>("PasswordHash")
                    .IsRequired()
                    .HasColumnType("varbinary(max)");

                b.Property<string>("PasswordResetToken")
                    .HasColumnType("nvarchar(max)");

                b.Property<byte[]>("PasswordSalt")
                    .IsRequired()
                    .HasColumnType("varbinary(max)");

                b.Property<DateTime?>("ResetTokenExpires")
                    .HasColumnType("datetime2");

                b.Property<string>("UserName")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("Models.Token.Token", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("AccessToken")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("AccessTokenExpires")
                    .HasColumnType("datetime2");

                b.Property<string>("RefreshToken")
                    .HasColumnType("nvarchar(max)");


                b.Property<DateTime?>("RefreshTokenExpires")
                    .HasColumnType("datetime2");

               

                b.HasKey("Id");

                b.ToTable("Tokens");
            });


            modelBuilder.Entity("Models.OrderDetail.OrderDetail", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("OrderId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("ItemCount")
                    .HasColumnType("int");
                b.Property<int>("ItemCanceled")
                    .HasColumnType("int");
                b.Property<string>("CancelNote")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");
                b.Property<string>("MenuItemId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("StoreEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserEmail")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("OrderDetails");
            });
#pragma warning restore 612, 618
        }
    }
}