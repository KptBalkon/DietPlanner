using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands.Users
{
    public class AddWeightPoint: AuthenticatedCommandBase
    {
        public DateTime WeightDate;
        public int Weight;
    }
}
