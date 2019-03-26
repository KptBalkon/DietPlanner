using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands.Users
{
    public class DeleteUser: AuthenticatedCommandBase
    {
        public Guid UserId { get; set; }
    }
}
