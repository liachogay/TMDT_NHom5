using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebMarket.Entities
{
    public partial class WebMarketContext : DbContext
    {
        public WebMarketContext()
        {
        }

        public WebMarketContext(DbContextOptions<WebMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Admininfo> Admininfo { get; set; }
        public virtual DbSet<Background> Background { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Orderdetail> Orderdetail { get; set; }
        public virtual DbSet<Orderupdate> Orderupdate { get; set; }
        public virtual DbSet<Priceupdate> Priceupdate { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Productdetail> Productdetail { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SSH4RF4\\SQLEXPRESS;Initial Catalog=WebMarket;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__account__F3DBC57294F21C36")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Admininfo>(entity =>
            {
                entity.ToTable("admininfo");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__admininf__F3DBC5723AB925E5")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Background>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("background");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__category__72E12F1BD471E47F")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_fk0");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.DeliveryDate)
                    .HasColumnName("delivery_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdAdmin).HasColumnName("ID_admin");

                entity.Property(e => e.IdCustomer).HasColumnName("ID_customer");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(50);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentType)
                    .HasColumnName("payment_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ShipCost).HasColumnName("ship_cost");

                entity.Property(e => e.ShippingType)
                    .HasColumnName("shipping_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.IdAdmin)
                    .HasConstraintName("order_fk1");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_fk0");
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.ToTable("orderdetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.IdOrder).HasColumnName("ID_order");

                entity.Property(e => e.IdProduct).HasColumnName("ID_product");

                entity.Property(e => e.Newprice).HasColumnName("newprice");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Orderdetail)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderdetail_fk0");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Orderdetail)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderdetail_fk1");
            });

            modelBuilder.Entity<Orderupdate>(entity =>
            {
                entity.ToTable("orderupdate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateEnd).HasColumnName("date_end");
                entity.Property(e => e.DateEnd).HasComputedColumnSql("GENERATED ALWAYS AS ROW START");

                entity.Property(e => e.DateUpdate).HasColumnName("date_update");
                entity.Property(e => e.DateUpdate).HasDefaultValueSql("('9999-12-31 23:59:59.9999999')");

                entity.Property(e => e.IdAdmin).HasColumnName("ID_admin");

                entity.Property(e => e.IdOrder).HasColumnName("ID_order");

                entity.Property(e => e.NewStatus).HasColumnName("new_status");

                entity.Property(e => e.OldStatus).HasColumnName("old_status");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Orderupdate)
                    .HasForeignKey(d => d.IdAdmin)
                    .HasConstraintName("orderupdate_fk1");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Orderupdate)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderupdate_fk0");
            });

            modelBuilder.Entity<Priceupdate>(entity =>
            {
                entity.ToTable("priceupdate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateEnd).HasColumnName("date_end");
                entity.Property(e => e.DateEnd).HasComputedColumnSql("GENERATED ALWAYS AS ROW START");

                entity.Property(e => e.DateUpdate).HasColumnName("date_update");
                entity.Property(e => e.DateUpdate).HasDefaultValueSql("('9999-12-31 23:59:59.9999999')");

                entity.Property(e => e.IdAdmin).HasColumnName("ID_admin");

                entity.Property(e => e.IdProduct).HasColumnName("ID_product");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Priceupdated).HasColumnName("priceupdated");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Priceupdate)
                    .HasForeignKey(d => d.IdAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("priceupdate_fk1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Priceupdate)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("priceupdate_fk0");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__product__72E12F1B5904C2EE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasDefaultValueSql("('Empty')");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.IdProvider).HasColumnName("ID_provider");

                entity.Property(e => e.IdType).HasColumnName("ID_type");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.QuantitySold)
                    .HasColumnName("quantity_sold")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.QuantityStock)
                    .HasColumnName("quantity_stock")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Visible')");

                entity.HasOne(d => d.IdProviderNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdProvider)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("product_fk0");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_fk1");
            });

            modelBuilder.Entity<Productdetail>(entity =>
            {
                entity.ToTable("productdetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EntryDate)
                    .HasColumnName("entry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Exp)
                    .HasColumnName("EXP")
                    .HasColumnType("date");

                entity.Property(e => e.IdAdmin).HasColumnName("ID_admin");

                entity.Property(e => e.IdProduct).HasColumnName("ID_product");

                entity.Property(e => e.IdWarehouse).HasColumnName("ID_warehouse");

                entity.Property(e => e.Mfg)
                    .HasColumnName("MFG")
                    .HasColumnType("date");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Productdetail)
                    .HasForeignKey(d => d.IdAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productdetail_fk1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Productdetail)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productdetail_fk0");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.ToTable("provider");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__provider__72E12F1BC1ABBAD4")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("type");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__type__72E12F1BF37E5653")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdCategory).HasColumnName("ID_category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Type)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("type_fk0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
