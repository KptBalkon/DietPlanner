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
        private readonly IMapper _mapper;

        public PlanService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PlanDTO> GetUserPlanAsync(string email)
        {
            User user = await _userRepository.GetAsync(email);
            if (user==null)
            {
                return null;
            }
            return _mapper.Map<Plan, PlanDTO>(user.Plan);
        }

        public async Task RegisterUsersPlanAsync(string email, int plannedWeight, DateTime targetDate)
        {
            User user = await _userRepository.GetAsync(email);
            if (user==null)
            {
                return;
            }
            await _userRepository.AddUsersPlanAsync(user, plannedWeight, targetDate);
        }
    }
}
