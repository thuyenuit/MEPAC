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

        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryType> CategoryType { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<InventoryDeliveryVoucher> InventoryDeliveryVoucher { get; set; }
        public DbSet<InventoryDeliveryVoucherDetail> InventoryDeliveryVoucherDetail { get; set; }
        public DbSet<InventoryReceivingVoucher> InventoryReceivingVoucher { get; set; }
        public DbSet<InventoryReceivingVoucherDetail> InventoryReceivingVoucherDetail { get; set; }
        public DbSet<ManagementStock> ManagementStock { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Menu> Mennu { get; set; }
        public DbSet<SubMenu> SubMenu { get; set; }
        public DbSet<MenuPermission> MenuPermission { get; set; }
        public DbSet<MenuPermissionDetail> MenuPermissionDetail { get; set; }

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
