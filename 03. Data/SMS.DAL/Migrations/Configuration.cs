namespace SMS.DAL.Migrations
{
    using DbContext;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SMS.DAL.DbContext.SMSDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SMSDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            CreateUserAdmin(context);
        }

        private void CreateUserAdmin(SMSDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SMSDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SMSDbContext()));

            var user = new ApplicationUser()
            {
                //UserCode = "admin",
                UserName = "admin",
                Email = "thuyennv1004@gmail.com",
                EmailConfirmed = true,
                FirstName = "Thuyền",
                LastName = "Nguyễn",
                BirthDay = DateTime.Now,
                PhoneNumber = "0372102464",
                PhoneNumberConfirmed = true,
            };

            if (manager.Users.Count(x => x.UserName == "admin") == 0)
            {
                manager.Create(user, "12345aA@");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Quản trị viên" });
                    roleManager.Create(new IdentityRole { Name = "Quản lý" });
                    roleManager.Create(new IdentityRole { Name = "Nhân viên" });
                }

                var adminUser = manager.FindByEmail("thuyennv1004@gmail.com");
                manager.AddToRoles(adminUser.Id, new string[] { "Quản trị viên", "Quản lý", "Nhân viên" });
            }

        }
    }
}
