using System.Web.Http;
using Adbrain.FullWebStack.Domain.Interfaces;
using Adbrain.FullWebStack.Infrastructure.Data;
using Adbrain.FullWebStack.Service.Interfaces;
using Adbrain.FullWebStack.Services;
using Adbrain.FullWebStack.Infrastructure.Bootstrapper;
using Autofac;
using Autofac.Integration.WebApi;
using Adbrain.FullWebStack.Web.Controllers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IocConfig), "RegisterDependencies")]

namespace Adbrain.FullWebStack.Infrastructure.Bootstrapper
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(typeof(SportsController).Assembly);

            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<EFDbContext>().As<IDbContext>();
            builder.RegisterType<PostRepository>().As<IPostRepository>();
            builder.RegisterType<RedditApiService>().As<IRedditApiService>();
            builder.RegisterType<PostService>().As<IPostService>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
