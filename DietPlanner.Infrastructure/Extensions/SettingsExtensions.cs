using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Extensions
{
    public static class SettingsExtensions
    {
        public static T GetSettings<T>(this IConfiguration configuration) where T : new() //where T -> Musi być to klasa i zawierać domyślny konstruktor publiczny
        {
            var section = typeof(T).Name.Replace("Settings", string.Empty); //pobieramy nazwę typu, z jego nazwy wywalamy zakończenie "Settings"
            var configurationValue = new T(); //np. typ GeneralSettings
            configuration.GetSection(section).Bind(configurationValue); // Pobierz z konfiguracji sekcję i zbinduj ją (Dla GeneralSettings będzie to sekcja General i przywiąż ją do typu GeneralSettings) 

            return configurationValue;
        }
    }
}
