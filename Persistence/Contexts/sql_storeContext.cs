using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Supermarket.API.Domain.Models;

#nullable disable

namespace Supermarket.API.Persistence.Contexts
{
    public partial class sql_storeContext : DbContext
    {
        public sql_storeContext()
        {
        }

        public sql_storeContext(DbContextOptions<sql_storeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderItemNote> OrderItemNotes { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            // #warning To protect potentially sensitive information in your connection string,
            // you should move it out of source code. You can avoid scaffolding the connection string
            // by using the Name= syntax to read it from configuration
            // - see https://go.microsoft.com/fwlink/?linkid=2131148.
            // For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("state")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerId, "fk_orders_customers_idx");

                entity.HasIndex(e => e.Status, "fk_orders_order_statuses_idx");

                entity.HasIndex(e => e.ShipperId, "fk_orders_shippers_idx");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Comments)
                    .HasMaxLength(2000)
                    .HasColumnName("comments");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("order_date");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("date")
                    .HasColumnName("shipped_date");

                entity.Property(e => e.ShipperId).HasColumnName("shipper_id");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint")
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orders_customers");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("fk_orders_shippers");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orders_order_statuses");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PRIMARY");

                entity.ToTable("order_items");

                entity.HasIndex(e => e.ProductId, "fk_order_items_products_idx");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(4,2)")
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_items_orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_items_products");
            });

            modelBuilder.Entity<OrderItemNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PRIMARY");

                entity.ToTable("order_item_notes");

                entity.Property(e => e.NoteId).HasColumnName("note_id");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("note");

                entity.Property(e => e.OrderId).HasColumnName("order_Id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("order_statuses");

                entity.Property(e => e.OrderStatusId)
                    .HasColumnType("tinyint")
                    .HasColumnName("order_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(4,2)")
                    .HasColumnName("unit_price");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shippers");

                entity.Property(e => e.ShipperId).HasColumnName("shipper_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
