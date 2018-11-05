﻿// <auto-generated />
using System;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyEat.Migrations
{
    [DbContext(typeof(EatContext))]
    [Migration("20181105090759_menuFix")]
    partial class menuFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyEat.Models.Cart", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerID");

                    b.Property<int>("AddressId")
                        .HasColumnName("AddressID");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<int>("MealTimeId")
                        .HasColumnName("MealTimeID");

                    b.Property<int>("TotalCaloricValue");

                    b.HasKey("CustomerId")
                        .HasName("pk_Cart");

                    b.HasIndex("AddressId");

                    b.HasIndex("MealTimeId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("EasyEat.Models.CartPart", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnName("DishID");

                    b.Property<int>("CartId")
                        .HasColumnName("CartID");

                    b.Property<int>("DishCount");

                    b.Property<int>("DishTemperature");

                    b.HasKey("DishId", "CartId")
                        .HasName("pk_CartPart");

                    b.HasIndex("CartId");

                    b.ToTable("CartPart");
                });

            modelBuilder.Entity("EasyEat.Models.Culture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Cultures");
                });

            modelBuilder.Entity("EasyEat.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Balance");

                    b.Property<int?>("CaloricGoal");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int?>("FoodStyleId")
                        .HasColumnName("FoodStyleID");

                    b.Property<string>("IdentityId");

                    b.Property<int>("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<long>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("FoodStyleId");

                    b.HasIndex("IdentityId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("EasyEat.Models.DeliveryAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerID");

                    b.Property<int>("FlatNamber");

                    b.Property<int>("HouseNamber");

                    b.Property<string>("Streete")
                        .IsRequired();

                    b.Property<double>("Xcoordinate");

                    b.Property<double>("Ycoordinate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("DeliveryAddress");
                });

            modelBuilder.Entity("EasyEat.Models.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DishDescription");

                    b.Property<string>("DishName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Dish");
                });

            modelBuilder.Entity("EasyEat.Models.FavouriteDish", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerID");

                    b.Property<int>("DishId")
                        .HasColumnName("DishID");

                    b.HasKey("CustomerId", "DishId")
                        .HasName("pk_FavouriteDish");

                    b.HasIndex("DishId");

                    b.ToTable("FavouriteDish");
                });

            modelBuilder.Entity("EasyEat.Models.FoodOrder", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("AddressId")
                        .HasColumnName("AddressID");

                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerID");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<int>("TotalCost");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("FoodOrder");
                });

            modelBuilder.Entity("EasyEat.Models.FoodStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaloricValue");

                    b.Property<string>("FoodStyleName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FoodStyle");
                });

            modelBuilder.Entity("EasyEat.Models.FoodStyleProduct", b =>
                {
                    b.Property<int>("FoodStyleId")
                        .HasColumnName("FoodStyleID");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.HasKey("FoodStyleId", "ProductId")
                        .HasName("pk_FoodStyleProduct");

                    b.HasIndex("ProductId");

                    b.ToTable("FoodStyleProduct");
                });

            modelBuilder.Entity("EasyEat.Models.Ingredient", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnName("DishID");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.Property<int>("ProductWeight");

                    b.HasKey("DishId", "ProductId")
                        .HasName("pk_Ingredient");

                    b.HasIndex("ProductId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("EasyEat.Models.MealTime", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("AllowedCaloricValue");

                    b.Property<TimeSpan>("MealTimestamp");

                    b.HasKey("Id");

                    b.ToTable("MealTime");
                });

            modelBuilder.Entity("EasyEat.Models.Menu", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnName("DishID");

                    b.Property<int>("RestaurantId")
                        .HasColumnName("RestaurantID");

                    b.Property<int>("Cost");

                    b.HasKey("DishId", "RestaurantId")
                        .HasName("pk_Menu");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("EasyEat.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaloricValue");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("EasyEat.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CultureId");

                    b.Property<string>("Key");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("EasyEat.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<int>("HouseNamber");

                    b.Property<int>("IsDeleted");

                    b.Property<string>("RestaurantName")
                        .IsRequired();

                    b.Property<string>("Streete")
                        .IsRequired();

                    b.Property<double>("Xcoordinate");

                    b.Property<double>("Ycoordinate");

                    b.HasKey("Id");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("EasyEat.Models.SpecialProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerID");

                    b.Property<int>("Allowance");

                    b.HasKey("ProductId", "CustomerId")
                        .HasName("pk_SpecialProduct");

                    b.HasIndex("CustomerId");

                    b.ToTable("SpecialProduct");
                });

            modelBuilder.Entity("EasyEat.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EasyEat.Models.Cart", b =>
                {
                    b.HasOne("EasyEat.Models.DeliveryAddress", "Address")
                        .WithMany("Cart")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK_Cart_AddressID");

                    b.HasOne("EasyEat.Models.Customer", "Customer")
                        .WithOne("Cart")
                        .HasForeignKey("EasyEat.Models.Cart", "CustomerId")
                        .HasConstraintName("FK_Cart_CustomerID");

                    b.HasOne("EasyEat.Models.MealTime", "MealTime")
                        .WithMany("Cart")
                        .HasForeignKey("MealTimeId")
                        .HasConstraintName("FK_Cart_MealTimeID");
                });

            modelBuilder.Entity("EasyEat.Models.CartPart", b =>
                {
                    b.HasOne("EasyEat.Models.Cart", "Cart")
                        .WithMany("CartPart")
                        .HasForeignKey("CartId")
                        .HasConstraintName("FK_CartPart_CartID");

                    b.HasOne("EasyEat.Models.Dish", "Dish")
                        .WithMany("CartPart")
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK_CartPart_DishID");
                });

            modelBuilder.Entity("EasyEat.Models.Customer", b =>
                {
                    b.HasOne("EasyEat.Models.FoodStyle", "FoodStyle")
                        .WithMany("Customer")
                        .HasForeignKey("FoodStyleId")
                        .HasConstraintName("FK_FoodStyleID");

                    b.HasOne("EasyEat.Models.User", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");
                });

            modelBuilder.Entity("EasyEat.Models.DeliveryAddress", b =>
                {
                    b.HasOne("EasyEat.Models.Customer", "Customer")
                        .WithMany("DeliveryAddress")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_DeliveryAddress_CustomerID");
                });

            modelBuilder.Entity("EasyEat.Models.FavouriteDish", b =>
                {
                    b.HasOne("EasyEat.Models.Customer", "Customer")
                        .WithMany("FavouriteDish")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_FavouriteDish_CustomerID");

                    b.HasOne("EasyEat.Models.Dish", "Dish")
                        .WithMany("FavouriteDish")
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK_FavouriteDish_DishID");
                });

            modelBuilder.Entity("EasyEat.Models.FoodOrder", b =>
                {
                    b.HasOne("EasyEat.Models.DeliveryAddress", "Address")
                        .WithMany("FoodOrder")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK_FoodOrder_AddressID");

                    b.HasOne("EasyEat.Models.Customer", "Customer")
                        .WithMany("FoodOrder")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_FoodOrder_CustomerID");
                });

            modelBuilder.Entity("EasyEat.Models.FoodStyleProduct", b =>
                {
                    b.HasOne("EasyEat.Models.FoodStyle", "FoodStyle")
                        .WithMany("FoodStyleProduct")
                        .HasForeignKey("FoodStyleId")
                        .HasConstraintName("FK_FoodStyleProduct_FoodStyleID");

                    b.HasOne("EasyEat.Models.Product", "Product")
                        .WithMany("FoodStyleProduct")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_FoodStyleProduct_ProductID");
                });

            modelBuilder.Entity("EasyEat.Models.Ingredient", b =>
                {
                    b.HasOne("EasyEat.Models.Dish", "Dish")
                        .WithMany("Ingredient")
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK_Ingredient_DishID");

                    b.HasOne("EasyEat.Models.Product", "Product")
                        .WithMany("Ingredient")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Ingredient_ProductID");
                });

            modelBuilder.Entity("EasyEat.Models.Menu", b =>
                {
                    b.HasOne("EasyEat.Models.Dish", "Dish")
                        .WithMany("Menu")
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK_Menu_DishID");

                    b.HasOne("EasyEat.Models.Restaurant", "Restaurant")
                        .WithMany("Menu")
                        .HasForeignKey("RestaurantId")
                        .HasConstraintName("FK_Menu_RestaurantID");
                });

            modelBuilder.Entity("EasyEat.Models.Resource", b =>
                {
                    b.HasOne("EasyEat.Models.Culture", "Culture")
                        .WithMany("Resources")
                        .HasForeignKey("CultureId");
                });

            modelBuilder.Entity("EasyEat.Models.SpecialProduct", b =>
                {
                    b.HasOne("EasyEat.Models.Customer", "Customer")
                        .WithMany("SpecialProduct")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_SpecialProduct_CustomerID");

                    b.HasOne("EasyEat.Models.Product", "Product")
                        .WithMany("SpecialProduct")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_SpecialProduct_ProductID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EasyEat.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EasyEat.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EasyEat.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EasyEat.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
