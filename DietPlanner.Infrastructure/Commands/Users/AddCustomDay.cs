using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands.Users
{
    public class AddCustomDay : AuthenticatedCommandBase
    {
        public DateTime date { get; set; }
        public int calories { get; set; }
    }
}
