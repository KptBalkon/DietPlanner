using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Services
{
    public interface IPlanCalculatorService
    {
        Task<Dictionary<DateTime, int>> CalculatePlan(Guid userId, int activityLevel);
    }
}
