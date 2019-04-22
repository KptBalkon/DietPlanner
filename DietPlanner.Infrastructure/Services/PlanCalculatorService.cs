using DietPlanner.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Services
{
    public class PlanCalculatorService : IPlanCalculatorService
    {
        private readonly IUserRepository _userRepository;

        public PlanCalculatorService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="activityLevel">Activity level 1-5</param>
        /// <param name="height">Height in cm</param>
        /// <returns></returns>
        public async Task<Dictionary<DateTime, int>> CalculatePlan(Guid userId, int activityLevel)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null) throw new Exception("User has not been found");
            if (user.Plan == null) throw new Exception("User has no plan");
            if (!user.WeightPoints.Any()) throw new Exception("User has no weightpoints");

            int currentWeight = user.WeightPoints.OrderByDescending(wp => wp.CreatedAt).First().Weight;
            int bmr = this.CaluclateBMR(currentWeight, user.Height, user.Age, user.Sex);
            int tdee = (int)(bmr * 1.025 + activityLevel * 0.175);
            int toLose = currentWeight - user.Plan.PlannedWeight;
            int daysCount = CountDaysTo(user.Plan.TargetDate);
            int nonCustomDaysCount = daysCount - user.Plan.CustomDays.Count;

            int totalCalories = (daysCount * tdee) - (toLose * 7500);
            int totalCaloriesNonCustom = totalCalories - user.Plan.CustomDays.Sum(c => c.Calories);
            int nonCustomTdee = (int)(totalCaloriesNonCustom / nonCustomDaysCount);

            Dictionary<DateTime, int> resultPlan = new Dictionary<DateTime, int>(daysCount);

            for(int i = 0; i<daysCount; i++)
            {
                DateTime day = DateTime.Now.Date.AddDays(i);
                if (user.Plan.CustomDays.FirstOrDefault(dt=>dt.Date.Date == day) != null)
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
            return (DateTime.Now - targetDate).Days;
        }
    }
}
