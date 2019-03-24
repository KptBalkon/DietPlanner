using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public Guid Procurer { get; set; }
    }
}
