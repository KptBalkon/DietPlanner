using DietPlanner.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Services
{
    public interface IPlanService:IService
    {
        Task<PlanDTO> GetUserPlanAsync(Guid userId);
        Task RegisterUsersPlanAsync(Guid userId, int plannedWeight, DateTime targetDate);
        Task AddCustomDayAsync(Guid userId, DateTime day, int calories);
        Task<Dictionary<DateTime, int>> CalculatePlan(Guid userId, int activityLevel);
    }
}
