using Autofac;
using DietPlanner.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DietPlanner.Infrastructure.IoC.Modules
{
    public class ServiceModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(RepositoryModule) // tu może być dowolna klasa z infrastruktury, chcemy tylko wyłuskać assembly 
            .GetTypeInfo()
            .Assembly;

        builder.RegisterAssemblyTypes(assembly)
            .Where(x => x.IsAssignableTo<IService>()) //przeskanuj assembly w poszukiwaniu typów które są przypisywalne do IService
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
}
