using APW.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APW.Data.MSSQL;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Models.Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ProductDB;User Id=sa;Password=*APW1234;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B4B112A34");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D330A37FC1");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED22E2B857");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__00200768");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Products)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__Products__Invent__01142BA1");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Products__Suppli__02084FDA");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3ACC640AFC");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(255);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666941C11848D");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.ContactName).HasMaxLength(255);
            entity.Property(e => e.ContactTitle).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.SupplierName).HasMaxLength(255);
        });

        modelBuilder.Entity<APW.Models.Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3214EC07C4C8D556");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC87ECE01F");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
