using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DietPlanner.Core.Domain
{
    public class Plan
    {
        private ISet<CustomDay> _customDays = new HashSet<CustomDay>();

        public Guid PlanId { get; protected set; }
        public int PlannedWeight { get; protected set; }
        public DateTime TargetDate { get; protected set; }
        public ISet<CustomDay> CustomDays => _customDays;

        public Guid UserId { get; set; }
        public User User { get; set; }

        protected Plan()
        {

        }

        protected Plan(Guid userId, int plannedWeight, DateTime targetDate)
        {
            PlanId = Guid.NewGuid();
            UserId = userId;
            SetPlannedWeight(plannedWeight);
            SetTargetDate(targetDate);
        }

        public static Plan Create(Guid userId, int plannedWeight, DateTime targetDate)
            => new Plan(userId, plannedWeight, targetDate);

        protected void SetPlannedWeight(int plannedWeight)
        {
            if (plannedWeight<0)
            {
                throw new Exception("You cannot plan to have negative weight");
            }
            PlannedWeight = plannedWeight;
        }

        protected void SetTargetDate(DateTime targetDate)
        {
            if(targetDate.Date < DateTime.UtcNow.Date)
            {
                throw new Exception("Please provide future date");
            }
            TargetDate = targetDate;
        }
        public void AddCustomDay(CustomDay customDay)
        {
            
            if(customDay.Calories<0)
            {
                throw new Exception("You can't eat less than 0 calories, adding training will be available in future releases.");
            }

            if(CustomDays.Where(d => d.Date.Date == customDay.Date.Date).Any())
            {
                throw new Exception($"This day has already been added for user with id {UserId}.");
            }
            else _customDays.Add(customDay);
        }

        public void PutCustomDay(CustomDay customDay)
        {
            if (customDay.Calories < 0)
            {
                throw new Exception("You can't eat less than 0 calories, adding training will be available in future releases.");
            }
            if (CustomDays.Where(d => d.Date.Date == customDay.Date.Date).Any())
            {
                _customDays.Single(d => d.Date.Date == customDay.Date.Date).SetCalories(customDay.Calories);
            }
            else _customDays.Add(customDay);
        }
    }
}
