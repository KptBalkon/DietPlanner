using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands.Users
{
    public class AddUserPlan : AuthenticatedCommandBase
    {
        public string email { get; set; }
        public int plannedWeight { get; set; }
        public DateTime targetDate { get; set; }
    }
}
