using Adbrain.WebApi.Models.Services;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adbrain.WebApi.App_Start.DependencyInjection
{
    public class WebApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPersonService>().To<PersonService>().InRequestScope();
        }
    }
}