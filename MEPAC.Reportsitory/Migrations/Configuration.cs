namespace MEPAC.Reportsitory.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MEPAC.Reportsitory.MEPACDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MEPAC.Reportsitory.MEPACDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // CreateMenu(context);
            // CreateSubMenu(context);
            // CreateUserAdmin(context);
        }

        private void CreateUserAdmin(MEPACDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MEPACDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new MEPACDbContext()));

            var user = new ApplicationUser()
            {
                UserCode = "admin",
                UserName = "admin",
                Email = "info@mepac.vn",
                EmailConfirmed = true,
                FullName = "Quản Trị Viên",
                FirstName = "Viên",
                BirthDay = DateTime.Now,
                PhoneNumber = "0862639900",
                PhoneNumberConfirmed = true,
            };

            if (manager.Users.Count(x => x.UserName == "admin") == 0)
            {
                manager.Create(user, "12345aA@");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "Manage" });
                }

                var adminUser = manager.FindByEmail("info@mepac.vn");
                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "Manage"});
            }
        }

        void CreateMenu(MEPACDbContext context)
        {
            context.Menu.AddOrUpdate(x => x.Display,
                  new Menu { Display = "Trang chủ", Order = 1, Router = "", IsSubMenu = 0, IsActive = true },
                  new Menu { Display = "Giới thiệu", Order = 2, Router = "", IsSubMenu = 0, IsActive = true },
                  new Menu { Display = "Lĩnh vực", Order = 3, Router = "", IsSubMenu = 1, IsActive = true },
                  new Menu { Display = "Dự án", Order = 4, Router = "", IsSubMenu = 0, IsActive = true },
                  new Menu { Display = "Tin tức", Order = 5, Router = "", IsSubMenu = 1, IsActive = true },
                  new Menu { Display = "Tuyển dụng", Order = 6, Router = "", IsSubMenu = 0, IsActive = true },
                  new Menu { Display = "Liên hệ", Order = 7, Router = "", IsSubMenu = 0, IsActive = true }
                        );
        }

        void CreateSubMenu(MEPACDbContext context)
        {
            context.SubMenu.AddOrUpdate(x => x.SubMenuID,
                  new SubMenu { Display = "Cung cấp - Lắp đặt HT Cơ Điện", Order = 1, Router = "", MenuID = 3, IsActive = true },
                  new SubMenu { Display = "Bảo trì - Bảo dưỡng HT Cơ Điện", Order = 2, Router = "", MenuID = 3, IsActive = true },
                  new SubMenu { Display = "Tư vấn thiết kế HT Cơ Điện", Order = 3, Router = "", MenuID = 3, IsActive = true },
                  new SubMenu { Display = "Tư vấn QLDA HT Cơ Điện", Order = 4, Router = "", MenuID = 3, IsActive = true },
                  new SubMenu { Display = "Tin công trường", Order = 1, Router = "", MenuID = 5, IsActive = true },
                  new SubMenu { Display = "Tin thị trường", Order = 2, Router = "", MenuID = 5, IsActive = true }
                        );
        }
    }
}
