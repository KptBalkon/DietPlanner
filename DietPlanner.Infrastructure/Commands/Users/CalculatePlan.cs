using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands.Users
{
    public class CalculatePlan: AuthenticatedCommandBase
    {
        public int activityLevel;
    }
}
