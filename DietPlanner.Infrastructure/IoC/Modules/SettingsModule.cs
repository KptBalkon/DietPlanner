using Autofac;
using DietPlanner.Infrastructure.EF;
using DietPlanner.Infrastructure.Extensions;
using DietPlanner.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DietPlanner.Infrastructure.IoC.Modules
{
    public class SettingsModule: Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                .SingleInstance(); //SingleInstance ponieważ potrzebujemy singletona do konfiguracji dla całej appki
            builder.RegisterInstance(_configuration.GetSettings<AuthenticationSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<SqlSettings>())
                .SingleInstance();
        }
    }
}
