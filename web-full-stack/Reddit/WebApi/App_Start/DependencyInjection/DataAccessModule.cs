using Adbrain.Reddit.DataAccess.DbContexts;
using Adbrain.Reddit.DataAccess.Repositories;
using Adbrain.Reddit.DataAccess.Wrappers;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.Reddit.WebApi.App_Start.DependencyInjection
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClock>().To<SystemClock>().InSingletonScope();
            Bind<ISqlDbContext>().To<SqlDbContext>().InRequestScope();
            Bind<IRawDataRepository>().To<RawDataRepository>().InRequestScope();
        }
    }
}