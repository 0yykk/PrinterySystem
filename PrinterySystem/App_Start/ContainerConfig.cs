﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Http;
using Printery.Data.Contexts;
using Printery.Data.Respositories;
using Printery.Provider.Provider;

namespace PrinterySystem.App_Start
{
    public class ContainerConfig
    {
        public static IContainer Container { get; set; }
        public static void RegisterComponets()
        {
            var builder = new ContainerBuilder();
            //Context
            builder.RegisterType<PrinteryContext>().As<IPrinteryContext>().InstancePerRequest();

            //Repositories
            builder.RegisterType<OrderRespository>().As<IOrderRespository>().InstancePerRequest();
            

            //Provider
            builder.RegisterType<OrderProvider>().As<IOrderProvider>().InstancePerRequest();
            //other
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //builder the Container
            var config = GlobalConfiguration.Configuration;
            var container = builder.Build();
            var dependencyResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(dependencyResolver);
        }
    }
}