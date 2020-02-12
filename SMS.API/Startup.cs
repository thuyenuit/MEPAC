using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SMS.DAL.DbContext;
using SMS.DAL.Infrastructure.Implements;
using SMS.DAL.Infrastructure.Interfaces;
using SMS.DAL.Repositories;
using SMS.Model.Models;
using SMS.Service.IServices;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(SMS.API.Startup))]

namespace SMS.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigAutofac(app);
        }

        public void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            // register web controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // register web api controller
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // register dbcontext
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<SMSDbContext>().AsSelf().InstancePerRequest();
            // ASP.NET Identity
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(x => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(x => app.GetDataProtectionProvider()).InstancePerRequest();

            #region // repository
            builder.RegisterAssemblyTypes(typeof(ProductRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            /*builder.RegisterAssemblyTypes(typeof(ApplicationUserRepository).Assembly)
               .Where(x => x.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(CategoryRepository).Assembly)
               .Where(x => x.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();*/
            #endregion

            #region // service
            builder.RegisterAssemblyTypes(typeof(IProductService).Assembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
            /*builder.RegisterAssemblyTypes(typeof(IApplicationUserService).Assembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();*/
            #endregion

            Autofac.IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);

        }
    }
}
