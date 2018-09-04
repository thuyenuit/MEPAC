using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MEPAC.Business.Business;
using MEPAC.Model.Models;
using MEPAC.Reportsitory;
using MEPAC.Reportsitory.Infrastructure.Implements;
using MEPAC.Reportsitory.Infrastructure.Interfaces;
using MEPAC.Reportsitory.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(MEPAC.Web.Startup))]
namespace MEPAC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);
            ConfigurationAuth(app);
        }

        public void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // register web api controller
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<MEPACDbContext>().AsSelf().InstancePerRequest();

            // ASP.NET Identity
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(x => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(x => app.GetDataProtectionProvider()).InstancePerRequest();

            #region // repository
            builder.RegisterAssemblyTypes(typeof(ApplicationUserRepository).Assembly)
               .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ContactRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(HiringRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(InformationRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(MenuRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(MetaImageRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(NewsRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(PartnerRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ProjectsRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(RangeRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(SlideRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(SubMenuRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            #endregion

            #region // business
            builder.RegisterAssemblyTypes(typeof(ApplicationUserBusiness).Assembly)
                .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ContactBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(HiringBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(InformationBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(MenuBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(MetaImageBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(NewsBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(PartnerBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ProjectsBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(RangeBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(SlideBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(SubMenuBusiness).Assembly)
               .Where(x => x.Name.EndsWith("Business")).AsImplementedInterfaces().InstancePerRequest();
            #endregion

            Autofac.IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }
    }
}
