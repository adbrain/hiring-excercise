using Adbrain.DataAccess.DbContexts;
using Adbrain.DataAccess.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.WebApi.App_Start.DependencyInjection
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISqlDbContext>().To<SqlDbContext>().InRequestScope();
            Bind<IPersonNodeRepository>().To<PersonNodeRepository>().InRequestScope();
        }
    }
}