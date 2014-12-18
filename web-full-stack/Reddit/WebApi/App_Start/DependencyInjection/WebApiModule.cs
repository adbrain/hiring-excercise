using Adbrain.Reddit.WebApi.Models.Services;
using Adbrain.Reddit.WebApi.Models.Wrappers;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.App_Start.DependencyInjection
{
    public class WebApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWebClientWrapper>().To<WebClientWrapper>().InTransientScope();
            Bind<IRedditSportsService>().To<RedditSportsService>().InTransientScope();
        }
    }
}