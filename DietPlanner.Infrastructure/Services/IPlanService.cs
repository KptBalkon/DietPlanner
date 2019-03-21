using DietPlanner.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Services
{
    public interface IPlanService:IService
    {
        Task<PlanDTO> GetUserPlanAsync(string email);
        Task RegisterUsersPlanAsync(string email, int plannedWeight, DateTime targetDate);
    }
}
