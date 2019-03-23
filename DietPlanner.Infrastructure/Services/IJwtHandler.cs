using DietPlanner.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDTO CreateToken(Guid userId, string role);
    }
}
