using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
