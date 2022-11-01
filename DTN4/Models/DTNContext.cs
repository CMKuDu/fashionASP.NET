using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DTN4.ModelViews;

namespace DTN4.Models
{
    public partial class DTNContext : DbContext
    {
        public DTNContext()
        {
        }

        public DTNContext(DbContextOptions<DTNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<AttributesPrice> AttributesPrices { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<TransactStatus> TransactStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=DESKTOP-EIC6341;Database=DTN;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("date")
                    .HasColumnName("lastLogin");

                entity.Property(e => e.Passwork)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.HasKey(e => e.AttributesId);

                entity.Property(e => e.AttributesId).HasColumnName("attributesID");

                entity.Property(e => e.AttributesName)
                    .HasMaxLength(50)
                    .HasColumnName("attributesName");
            });

            modelBuilder.Entity<AttributesPrice>(entity =>
            {
                entity.ToTable("AttributesPrice");

                entity.Property(e => e.AttributesPriceId).HasColumnName("attributesPriceID");

                entity.Property(e => e.AttributesId).HasColumnName("attributesID");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.HasOne(d => d.Attributes)
                    .WithMany(p => p.AttributesPrices)
                    .HasForeignKey(d => d.AttributesId)
                    .HasConstraintName("FK_AttributesPrice_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AttributesPrices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_AttributesPrice_Product");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId);

                entity.Property(e => e.CatId).HasColumnName("catID");

                entity.Property(e => e.Alias).HasMaxLength(150);

                entity.Property(e => e.CatName)
                    .HasMaxLength(250)
                    .HasColumnName("catName");

                entity.Property(e => e.Cover).HasMaxLength(250);

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(200)
                    .HasColumnName("metaDesc");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(200)
                    .HasColumnName("metaKey");

                entity.Property(e => e.ParentId).HasColumnName("parentID");

                entity.Property(e => e.SchemaMarkup).HasColumnName("schemaMarkup");

                entity.Property(e => e.Thumb).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(150);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomId);

                entity.ToTable("Customer");

                entity.Property(e => e.CustomId).HasColumnName("customID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Avatar).HasMaxLength(100);

                entity.Property(e => e.BirthDay)
                    .HasColumnType("date")
                    .HasColumnName("birthDay");

                entity.Property(e => e.Create).HasColumnType("date");

                entity.Property(e => e.CustomName)
                    .HasMaxLength(50)
                    .HasColumnName("customName");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.LastLogin)
                    .HasColumnType("date")
                    .HasColumnName("lastLogin");

                entity.Property(e => e.LocationId).HasColumnName("locationID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Salt)
                    .HasMaxLength(8)
                    .IsFixedLength();

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Customer_Location");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("locationID");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(50)
                    .HasColumnName("locationName");

                entity.Property(e => e.NameWithType)
                    .HasMaxLength(100)
                    .HasColumnName("nameWithType");

                entity.Property(e => e.ParentCode).HasColumnName("parentCode");

                entity.Property(e => e.PathNwithType)
                    .HasMaxLength(200)
                    .HasColumnName("pathNWithType");

                entity.Property(e => e.Slup)
                    .HasMaxLength(100)
                    .HasColumnName("slup");

                entity.Property(e => e.Type).HasMaxLength(20);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.CustomId).HasColumnName("customID");

                entity.Property(e => e.OrderDate)
                    .HasMaxLength(10)
                    .HasColumnName("orderDate")
                    .IsFixedLength();

                entity.Property(e => e.PayDate)
                    .HasColumnType("date")
                    .HasColumnName("payDate");

                entity.Property(e => e.PaymentId).HasColumnName("paymentID");

                entity.Property(e => e.ShipDate)
                    .HasColumnType("date")
                    .HasColumnName("shipDate");

                entity.Property(e => e.TransactStatusId).HasColumnName("transactStatusID");

                entity.HasOne(d => d.Custom)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomId)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.TransactStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransactStatusId)
                    .HasConstraintName("FK_Order_TransactStatus");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId);

                entity.Property(e => e.OrderDetailsId).HasColumnName("orderDetailsID");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.OrderNumber)
                    .HasColumnType("date")
                    .HasColumnName("orderNumber");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.ShipDate)
                    .HasColumnType("date")
                    .HasColumnName("shipDate");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Order");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("Page");

                entity.Property(e => e.PageId).HasColumnName("pageID");

                entity.Property(e => e.Alias).HasMaxLength(100);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("createDate");

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(200)
                    .HasColumnName("metaDesc");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(200)
                    .HasColumnName("metaKey");

                entity.Property(e => e.PageName)
                    .HasMaxLength(250)
                    .HasColumnName("pageName");

                entity.Property(e => e.Thumb).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Alias).HasMaxLength(150);

                entity.Property(e => e.BestSeller).HasColumnName("bestSeller");

                entity.Property(e => e.CatId).HasColumnName("catID");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("date")
                    .HasColumnName("dateCreate");

                entity.Property(e => e.DateModified)
                    .HasColumnType("date")
                    .HasColumnName("dateModified");

                entity.Property(e => e.HomeFlag).HasColumnName("homeFlag");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(200)
                    .HasColumnName("metaKey");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(150)
                    .HasColumnName("productName");

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(150)
                    .HasColumnName("shortDesc");

                entity.Property(e => e.Thumb).IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.UntiStock).HasColumnName("untiStock");

                entity.Property(e => e.Video).HasMaxLength(100);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_Product_Categories");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shipper");

                entity.Property(e => e.ShipperId).HasColumnName("shipperID");

                entity.Property(e => e.Company).HasMaxLength(150);

                entity.Property(e => e.ShipDate)
                    .HasColumnType("date")
                    .HasColumnName("shipDate");

                entity.Property(e => e.ShipperName)
                    .HasMaxLength(50)
                    .HasColumnName("shipperName");

                entity.Property(e => e.ShipperPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("shipperPhone")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TinTuc>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.ToTable("TinTuc");

                entity.Property(e => e.PostId).HasColumnName("postID");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.Alias).HasMaxLength(150);

                entity.Property(e => e.Author).HasMaxLength(150);

                entity.Property(e => e.CatId).HasColumnName("catID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("createDate");

                entity.Property(e => e.IsHot).HasColumnName("isHot");

                entity.Property(e => e.IsNewFeed).HasColumnName("isNewFeed");

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(200)
                    .HasColumnName("metaDesc");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(200)
                    .HasColumnName("metaKey");

                entity.Property(e => e.Scontenct).HasColumnName("SContenct");

                entity.Property(e => e.Thumb).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(150);
            });

            modelBuilder.Entity<TransactStatus>(entity =>
            {
                entity.ToTable("TransactStatus");

                entity.Property(e => e.TransactStatusId).HasColumnName("transactStatusID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<DTN4.ModelViews.RegisterViewModel> RegisterViewModel { get; set; }
    }
}
