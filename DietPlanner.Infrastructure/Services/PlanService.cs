using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;
using DietPlanner.Infrastructure.DTO;

namespace DietPlanner.Infrastructure.Services
{
    public class PlanService : IPlanService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public PlanService(IUserRepository userRepository, IPlanRepository planRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<PlanDTO> GetUserPlanAsync(Guid userId)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user==null)
            {
                return null;
            }
            return _mapper.Map<Plan, PlanDTO>(user.Plan);
        }

        public async Task RegisterUsersPlanAsync(Guid userId, int plannedWeight, DateTime targetDate)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user==null)
            {
                return;
            }
            await _planRepository.AddPlan(Plan.Create(userId,plannedWeight,targetDate));
        }

        public async Task AddCustomDayAsync(Guid userId, DateTime day, int calories)
        {
            User user = await _userRepository.GetAsync(userId);
            if (user==null || user.Plan==null)
            {
                return;
            }
            await _planRepository.AddCustomDay(CustomDay.Create(user.Plan.PlanId, day, calories));
        }
    }
}
