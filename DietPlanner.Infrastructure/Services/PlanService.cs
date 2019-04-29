using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Dictionary<DateTime, int>> CalculatePlan(Guid userId, int activityLevel)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null) throw new Exception("User has not been found");
            if (user.Plan == null) throw new Exception("User has no plan");
            if (!user.WeightPoints.Any()) throw new Exception("User has no weightpoints");

            int currentWeight = user.WeightPoints.OrderByDescending(wp => wp.CreatedAt).First().Weight;
            int bmr = this.CaluclateBMR(currentWeight, user.Height, user.Age, user.Sex);
            int tdee = (int)(bmr * (1.025 + (activityLevel * 0.175))); 
            int toLose = currentWeight - user.Plan.PlannedWeight;
            int daysCount = CountDaysTo(user.Plan.TargetDate);
            int nonCustomDaysCount = daysCount - user.Plan.CustomDays.Count;

            int totalCalories = (daysCount * tdee) - (toLose * 7500);
            int totalCaloriesNonCustom = totalCalories - user.Plan.CustomDays.Sum(c => c.Calories);
            int nonCustomTdee = (int)(totalCaloriesNonCustom / nonCustomDaysCount);

            Dictionary<DateTime, int> resultPlan = new Dictionary<DateTime, int>(daysCount);

            for (int i = 0; i < daysCount; i++)
            {
                DateTime day = DateTime.Now.Date.AddDays(i);
                if (user.Plan.CustomDays.FirstOrDefault(dt => dt.Date.Date == day) != null)
                {
                    resultPlan[day] = user.Plan.CustomDays.FirstOrDefault(dt => dt.Date.Date == day).Calories;
                }
                else
                {
                    resultPlan[day] = nonCustomTdee;
                }
            }

            return resultPlan;
        }

        private int CaluclateBMR(int currentWeight, int height, int age, string sex)
        {
            if (sex == "Male")
            {
                return (66 + (int)(13.7 * currentWeight) + (int)(5 * height) - (int)(6.8 * age));
            }
            else if (sex == "Female")
            {
                return (655 + (int)(9.6 * currentWeight) + (int)(1.8 * height) - (int)(4.7 * age));
            }
            else
            {
                //Adding Male and Female bmr calculations and taking middle result.
                return (int)((66 + (int)(13.7 * currentWeight) + (int)(5 * height) - (int)(6.8 * age)) + (655 + (int)(9.6 * currentWeight) + (int)(1.8 * height) - (int)(4.7 * age))) / 2;
            }

        }

        private int CountDaysTo(DateTime targetDate)
        {
            return (targetDate - DateTime.Now).Days;
        }
    }
}
