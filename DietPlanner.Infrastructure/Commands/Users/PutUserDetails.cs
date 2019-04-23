using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands.Users
{
    public class PutUserDetails : AuthenticatedCommandBase
    {
        public int height { get; set; }
        public DateTime birthday { get; set; }
        public string sex { get; set; }
    }
}
