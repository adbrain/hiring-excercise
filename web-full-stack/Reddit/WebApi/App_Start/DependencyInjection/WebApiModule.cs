using Adbrain.Reddit.WebApi.Models.Helpers;
using Adbrain.Reddit.WebApi.Models.Services;
using Adbrain.Reddit.WebApi.Models.Wrappers;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.App_Start.DependencyInjection
{
    public class WebApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<NameValueCollection>().ToConstant(ConfigurationManager.AppSettings)
                .WhenInjectedExactlyInto<AppSettingsHelper>();
            Bind<IAppSettingsHelper>().To<AppSettingsHelper>().InSingletonScope();
            Bind<IDateTimeHelper>().To<DateTimeHelper>().InSingletonScope();
            
            Bind<IWebClientWrapper>().To<WebClientWrapper>().InTransientScope();
            Bind<IRedditSportsService>().To<RedditSportsService>().InTransientScope();
            Bind<IRedditJsonHelper>().To<RedditJsonHelper>().InSingletonScope();
        }
    }
}