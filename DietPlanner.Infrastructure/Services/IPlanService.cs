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
    }
}
