using Microsoft.AspNet.Identity.EntityFramework;
using SMS.Model.Models;
using System.Data.Entity;

namespace SMS.DAL.DbContext
{
    public class SMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public SMSDbContext()
            : base("SMSConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ColorProduct> ColorProducts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldGroup> FieldGroups { get; set; }
        public DbSet<FieldProduct> FieldProducts { get; set; }
        public DbSet<InventoryDeliveryVoucher> InventoryDeliveryVouchers { get; set; }
        public DbSet<InventoryDeliveryVoucherDetail> InventoryDeliveryVoucherDetails { get; set; }
        public DbSet<InventoryReceivingVoucher> InventoryReceivingVouchers { get; set; }
        public DbSet<InventoryReceivingVoucherDetail> InventoryReceivingVoucherDetails { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuPermission> MenuPermissions { get; set; }
        public DbSet<MenuPermissionDetail> MenuPermissionDetails { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockManager> StockManagers { get; set; }
        public DbSet<StockProduct> StockProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public static SMSDbContext Create()
        {
            return new SMSDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
            // create index
            //modelBuilder.Entity<Product>()
            //    .HasIndex("Index_Product", IndexOptions.Unique, e => e.Property(x => x.ProductID));
            //modelBuilder.Entity<Category>()
            //     .HasIndex("Index_Category", IndexOptions.Unique, e => e.Property(x => x.CategoryID));

        }
    }
}
