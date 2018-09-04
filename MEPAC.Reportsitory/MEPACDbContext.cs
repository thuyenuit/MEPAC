using MEPAC.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Reportsitory
{
    public class MEPACDbContext : IdentityDbContext<ApplicationUser>
    {
        public MEPACDbContext()
            : base("MEPACConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Hiring> Hiring { get; set; }
        public DbSet<MetaImage> MetaImage { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Range> Range { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<SubMenu> SubMenu { get; set; }
       
        public static MEPACDbContext Create()
        {
            return new MEPACDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);

            //base.OnModelCreating(modelBuilder);
            /* modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
             modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
             // create index
             modelBuilder.Entity<Product>()
                 .HasIndex("Index_Product", IndexOptions.Unique, e => e.Property(x => x.ProductID));
             modelBuilder.Entity<ProductCategory>()
                 .HasIndex("Index_ProductCategory", IndexOptions.Unique, e => e.Property(x => x.ProductCategoryID));*/

        }
    }
}
