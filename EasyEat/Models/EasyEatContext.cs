using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Models
{
    public class EasyEatContext:DbContext
    {

        public EasyEatContext(DbContextOptions<EasyEatContext> options) : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartPart> CartParts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<FavouriteDish> FavouriteDishes { get; set; }
        public virtual DbSet<FoodOrder> FoodOrders { get; set; }
        public virtual DbSet<FoodStyle> FoodStyle { get; set; }
        public virtual DbSet<FoodStyleProduct> FoodStyleProducts { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<MealTime> MealTimes { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<SpecialProduct> SpecialProducts { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("DefaultConnection");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("pk_Cart");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.MealTimeId).HasColumnName("MealTimeID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_AddressID");

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.Cart)
                    .HasForeignKey<Cart>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_CustomerID");

                entity.HasOne(d => d.MealTime)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.MealTimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_MealTimeID");
            });

            modelBuilder.Entity<CartPart>(entity =>
            {
                entity.HasKey(e => new { e.DishId, e.CartId })
                    .HasName("pk_CartPart");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartParts)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartPart_CartID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.CartParts)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartPart_DishID");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.FoodStyleId).HasColumnName("FoodStyleID");

                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(d => d.FoodStyle)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.FoodStyleId)
                    .HasConstraintName("FK_FoodStyleID");
            });

            modelBuilder.Entity<DeliveryAddress>(entity =>
            {
                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Country).IsRequired();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Streete).IsRequired();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.DeliveryAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryAddress_CustomerID");
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.Property(e => e.DishName).IsRequired();
            });

            modelBuilder.Entity<FavouriteDish>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.DishId })
                    .HasName("pk_FavouriteDish");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FavouriteDishes)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteDish_CustomerID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.FavouriteDishes)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteDish_DishID");
            });

            modelBuilder.Entity<FoodOrder>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.FoodOrders)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodOrder_AddressID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FoodOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodOrder_CustomerID");
            });

            modelBuilder.Entity<FoodStyle>(entity =>
            {
                entity.Property(e => e.FoodStyleName).IsRequired();
            });

            modelBuilder.Entity<FoodStyleProduct>(entity =>
            {
                entity.HasKey(e => new { e.FoodStyleId, e.ProductId })
                    .HasName("pk_FoodStyleProduct");

                entity.Property(e => e.FoodStyleId).HasColumnName("FoodStyleID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.FoodStyle)
                    .WithMany(p => p.FoodStyleProducts)
                    .HasForeignKey(d => d.FoodStyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodStyleProduct_FoodStyleID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.FoodStyleProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodStyleProduct_ProductID");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => new { e.DishId, e.ProductId })
                    .HasName("pk_Ingredient");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredient_DishID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredient_ProductID");
            });

            modelBuilder.Entity<MealTime>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => new { e.DishId, e.RestaurantId })
                    .HasName("pk_Menu");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_DishID");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_RestaurantID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductName).IsRequired();
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Country).IsRequired();

                entity.Property(e => e.RestaurantName).IsRequired();

                entity.Property(e => e.Streete).IsRequired();
            });

            modelBuilder.Entity<SpecialProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CustomerId })
                    .HasName("pk_SpecialProduct");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SpecialProducts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialProduct_CustomerID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SpecialProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialProduct_ProductID");
            });
        }
    }
}
