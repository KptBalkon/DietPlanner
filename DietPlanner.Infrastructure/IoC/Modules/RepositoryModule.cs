using Autofac;
using DietPlanner.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DietPlanner.Infrastructure.IoC.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule) // tu może być dowolna klasa z infrastruktury, chcemy tylko wyłuskać assembly 
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IRepository>()) //przeskanuj assembly w poszukiwaniu typów które są przypisywalne do IRepository
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
