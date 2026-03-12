using Microsoft.EntityFrameworkCore;
using APW.Data.Foodbankdb.Models;

namespace APW.Data.MSSQL;

public partial class FoodbankDbContext : DbContext
{
    public FoodbankDbContext()
    {
    }

    public FoodbankDbContext(DbContextOptions<FoodbankDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FoodItem> FoodItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodItem>(entity =>
        {
            entity.HasKey(e => e.FoodItemId).HasName("PK__FoodItem__464DCBF267501471");
            entity.HasIndex(e => e.Barcode, "UQ__FoodItem__177800D372C0EBDE").IsUnique();
            entity.Property(e => e.FoodItemId).HasColumnName("FoodItemID");
            entity.Property(e => e.Barcode).HasMaxLength(50);
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Ingredients).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsPerishable).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.QuantityInStock).HasDefaultValue(0);
            entity.Property(e => e.Supplier).HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}