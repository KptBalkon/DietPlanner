using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid Procurer { get; set; }
    }
}
