using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class WebEcommerceContext : DbContext
{
    public WebEcommerceContext()
    {
    }

    public WebEcommerceContext(DbContextOptions<WebEcommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<Galery> Galeries { get; set; }

    public virtual DbSet<Oder> Oders { get; set; }

    public virtual DbSet<OrderCancellationReason> OrderCancellationReasons { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DELL\\SQLHAU;Initial Catalog=Web_ecommerce;Persist Security Info=True;User ID=sa;Password=09012003;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandLogo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("brand_logo");
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.ToTable("Detail");

            entity.Property(e => e.DetailId).HasColumnName("detail_id");
            entity.Property(e => e.Color)
                .HasMaxLength(100)
                .HasColumnName("color");
            entity.Property(e => e.ConnectorPort)
                .HasMaxLength(100)
                .HasColumnName("connector_port");
            entity.Property(e => e.CpuGeneration)
                .HasMaxLength(150)
                .HasColumnName("CPU_generation");
            entity.Property(e => e.Keyboard)
                .HasMaxLength(100)
                .HasColumnName("keyboard");
            entity.Property(e => e.Os)
                .HasMaxLength(100)
                .HasColumnName("OS");
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.PartNumber)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("part_number");
            entity.Property(e => e.Pin)
                .HasMaxLength(100)
                .HasColumnName("pin");
            entity.Property(e => e.Screen)
                .HasMaxLength(150)
                .HasColumnName("screen");
            entity.Property(e => e.SeriesLaptop)
                .HasMaxLength(150)
                .HasColumnName("series_laptop");
            entity.Property(e => e.Size)
                .HasMaxLength(100)
                .HasColumnName("size");
            entity.Property(e => e.Storage)
                .HasMaxLength(100)
                .HasColumnName("storage");
            entity.Property(e => e.Weight)
                .HasMaxLength(50)
                .HasColumnName("weight");
            entity.Property(e => e.WirelessConnection)
                .HasMaxLength(100)
                .HasColumnName("wireless_connection");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Details)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detail_Product");
        });

        modelBuilder.Entity<Galery>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Galery");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.PId).HasColumnName("p_id");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Galeries)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Galery_Product");
        });

        modelBuilder.Entity<Oder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("Oder");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.OderDate)
                .HasColumnType("datetime")
                .HasColumnName("oder_date");
            entity.Property(e => e.OrderCancellationReasonId).HasColumnName("order_cancellation_reason_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.ShippingCost).HasColumnName("shipping_cost");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalMoney).HasColumnName("total_money");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.OrderCancellationReason).WithMany(p => p.Oders)
                .HasForeignKey(d => d.OrderCancellationReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Oder_Order_cancellation_reason");

            entity.HasOne(d => d.Payment).WithMany(p => p.Oders)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Oder_Payment");

            entity.HasOne(d => d.User).WithMany(p => p.Oders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Oder_User");
        });

        modelBuilder.Entity<OrderCancellationReason>(entity =>
        {
            entity.HasKey(e => e.ReasonId);

            entity.ToTable("Order_cancellation_reason");

            entity.Property(e => e.ReasonId).HasColumnName("reason_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(150)
                .HasColumnName("reason");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("Order_item");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.OderId).HasColumnName("oder_id");
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Oder).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_item_Oder");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_item_Product");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Method)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("method");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.TotalMoney).HasColumnName("total_money");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.ToTable("Price");

            entity.Property(e => e.PriceId)
                .ValueGeneratedNever()
                .HasColumnName("price_id");
            entity.Property(e => e.CreateAt).HasColumnName("create_at");
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.Price1).HasColumnName("price");
            entity.Property(e => e.UpdateAt).HasColumnName("update_at");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Prices)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Price_Price");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.PId);

            entity.ToTable("Product");

            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CreatAt).HasColumnName("creat_at");
            entity.Property(e => e.Description)
                .HasMaxLength(3000)
                .HasColumnName("description");
            entity.Property(e => e.Featured).HasColumnName("featured");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdateAt).HasColumnName("update_at");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Brand");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .HasColumnName("comment");
            entity.Property(e => e.CreatAt).HasColumnName("creat_at");
            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_Product");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User", tb => tb.HasTrigger("SetUpdateAtOnUpdate"));

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Avata)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("avata");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("create_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("passwordSalt");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("update_at");
            entity.Property(e => e.VerificationToken)
                .HasMaxLength(255)
                .HasColumnName("verificationToken");
            entity.Property(e => e.VerifiedAt).HasColumnName("verifiedAt");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
