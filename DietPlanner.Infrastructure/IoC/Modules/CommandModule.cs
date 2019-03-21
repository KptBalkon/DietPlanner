using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using DietPlanner.Infrastructure.Commands;

namespace DietPlanner.Infrastructure.IoC.Modules
{
    public class CommandModule : Autofac.Module
    { 
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule) // tu może być dowolna klasa z infrastruktury, chcemy tylko wyłuskać assembly 
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly) //przeskanuj assembly w poszukiwaniu typów które nas interesują, przeskanuj wszystkie klasy i interfejsy
                .AsClosedTypesOf(typeof(ICommandHandler<>)) // i zarejestruj im typy domykające (CommandHandler dopisze do ICommandHandler)
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>() // Dla IcommandDispatchera użyj CommandDispatchera
                .InstancePerLifetimeScope(); // Lifetime per request http dla użytkownika
            
        }
    }
}
