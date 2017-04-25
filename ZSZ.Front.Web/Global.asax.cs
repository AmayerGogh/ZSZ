using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using ZSZ.IService;
using ZSZ.Web.Common.Binder;
using ZSZ.Web.Common.Filter;

namespace ZSZ.Front.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //注册过滤器
            GlobalFilters.Filters.Add(new ZSZExceptionFilter());
            GlobalFilters.Filters.Add(new ZSZJsonNetActionFilter());

            //注册字符串类型Model属性绑定器
            ModelBinders.Binders.Add(typeof(string), new ZSZStringModelBinder());

            #region autofac注册
            var builder = new ContainerBuilder();
            //扩展方法：把当前程序集中的所有Controller都注册
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired(); //属性依赖
            //注册符合接口标识的服务
            Assembly[] assemblies = new Assembly[] { Assembly.Load("ZSZ.Service") };
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => !type.IsAbstract && typeof(IServiceSupport).IsAssignableFrom(type))
                .AsImplementedInterfaces().PropertiesAutowired(); //属性依赖
            //生成声明周期
            var container = builder.Build();

            //注册为默认依赖注入解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
        }
    }
}
