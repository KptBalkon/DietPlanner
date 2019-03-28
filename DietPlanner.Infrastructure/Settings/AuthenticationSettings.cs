using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Settings
{
    public class AuthenticationSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
