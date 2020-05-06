using DAL.Domains;
using DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class MediStockContext : DbContext
    {
        #region Ctor

        public MediStockContext(DbContextOptions<MediStockContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Role> CustomerRoles { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<CategoryMedicine> CategoryMedicine { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Mapping Theory Reference

            // Mapping reference 
            // https://www.learnentityframeworkcore.com/configuration/one-to-one-relationship-configuration
            // https://www.learnentityframeworkcore.com/configuration/one-to-many-relationship-configuration
            // https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration 

            #endregion

            #region Cart & CartOrder

            modelBuilder.Entity<Cart>()
                    .ToTable("Carts")
                    .HasMany(x => x.OrderItems)
                    .WithOne(x => x.Cart);

            modelBuilder.Entity<Cart>()
                .HasOne(x => x.Order)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey<Order>(x => x.ShoppingCartId);

            #endregion

            #region Category & Category Medicine

            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasMany(x => x.Categories).
                WithOne(x => x.SubCategory);

            modelBuilder.Entity<CategoryMedicine>()
                .HasKey(x => new { x.CategoryId, x.MedicineId });

            modelBuilder.Entity<CategoryMedicine>()
                .HasOne(x => x.Category)
                .WithMany(x => x.CategoryMedicine)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<CategoryMedicine>()
                .HasOne(x => x.Medicine)
                .WithMany(x => x.CategoryMedicine)
                .HasForeignKey(x => x.MedicineId);

            #endregion

            #region Customer, CustomerPassword & Customer Role

            modelBuilder.Entity<Customer>()
                    .ToTable("Customers")
                    .HasOne(x => x.Password)
                    .WithOne(x => x.Customer)
                    .HasForeignKey<Password>(x => x.CustomerId);

            modelBuilder.Entity<CustomerRole>()
                .HasKey(x => new { x.RoleId, x.CustomerId });

            modelBuilder.Entity<CustomerRole>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.CustomerRoles)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<CustomerRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.CustomerRoles)
                .HasForeignKey(x => x.RoleId);

            #endregion

            #region Password

            modelBuilder.Entity<Password>()
                    .ToTable("Passwords");

            #endregion

            #region Role

            modelBuilder.Entity<Role>()
                    .ToTable("Roles");

            #endregion

            #region Logs

            modelBuilder.Entity<Log>()
                    .ToTable("Logs");

            #endregion

            #region Orders

            modelBuilder.Entity<Order>()
                    .ToTable("Orders");

            #endregion

            #region OrderItems & Cart

            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItems")
                .HasOne<Cart>(x => x.Cart)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.Id);

            #endregion

            #region Pictures

            modelBuilder.Entity<Picture>()
                    .ToTable("Pictures");

            #endregion

            #region Medicines & MedicineStock

            modelBuilder.Entity<Medicine>()
                    .ToTable("Medicines")
                    .HasMany(x => x.Pictures)
                    .WithOne(x => x.Medicine);

            modelBuilder.Entity<Medicine>()
                .HasOne(x => x.Stock)
                .WithOne(x => x.Medicine)
                .HasForeignKey<Stock>(x => x.MedicineId);

            #endregion

            #region Stock

            modelBuilder.Entity<Stock>()
                    .ToTable("Stocks");

            #endregion

            #region General Settings

            modelBuilder.Entity<GeneralSetting>()
                    .ToTable("GeneralSettings"); 

            #endregion
        }

        #endregion
    }
}
