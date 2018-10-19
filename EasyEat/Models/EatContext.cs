using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EasyEat.Models
{
    public partial class EatContext : DbContext
    {
        public EatContext()
        {
        }

        public EatContext(DbContextOptions<EatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartPart> CartPart { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<DeliveryAddress> DeliveryAddress { get; set; }
        public virtual DbSet<Dish> Dish { get; set; }
        public virtual DbSet<FavouriteDish> FavouriteDish { get; set; }
        public virtual DbSet<FoodOrder> FoodOrder { get; set; }
        public virtual DbSet<FoodStyle> FoodStyle { get; set; }
        public virtual DbSet<FoodStyleProduct> FoodStyleProduct { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<MealTime> MealTime { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<SpecialProduct> SpecialProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LLK7E72\\SQLEXPRESS;Database=Eat;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

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
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_AddressID");

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.Cart)
                    .HasForeignKey<Cart>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_CustomerID");

                entity.HasOne(d => d.MealTime)
                    .WithMany(p => p.Cart)
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
                    .WithMany(p => p.CartPart)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartPart_CartID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.CartPart)
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
                    .WithMany(p => p.Customer)
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
                    .WithMany(p => p.DeliveryAddress)
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
                    .WithMany(p => p.FavouriteDish)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouriteDish_CustomerID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.FavouriteDish)
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
                    .WithMany(p => p.FoodOrder)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodOrder_AddressID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FoodOrder)
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
                    .WithMany(p => p.FoodStyleProduct)
                    .HasForeignKey(d => d.FoodStyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FoodStyleProduct_FoodStyleID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.FoodStyleProduct)
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
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredient_DishID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ingredient)
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
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_DishID");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Menu)
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
                    .WithMany(p => p.SpecialProduct)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialProduct_CustomerID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SpecialProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecialProduct_ProductID");
            });
        }
    }
}
