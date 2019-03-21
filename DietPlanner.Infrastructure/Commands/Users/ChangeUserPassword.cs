using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Commands.Users
{
    public class ChangeUserPassword : ICommand
    {
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
