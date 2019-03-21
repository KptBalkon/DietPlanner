using System;

namespace DietPlanner.Core.Domain
{
    public class CustomDay
    {
        public Guid CustomDayId { get; protected set; }
        public Guid PlanId { get; protected set; }
        public DateTime Date { get; protected set; }
        public int Calories { get; protected set; }

        protected CustomDay()
        {

        }

        protected CustomDay(Guid planId, DateTime date, int calories)
        {
            CustomDayId = Guid.NewGuid();
            PlanId = planId;
            Date = date;
            SetCalories(calories);
        }

        public void SetCalories(int calories)
        {
            Calories = calories;
        }

        public static CustomDay Create(Guid planId, DateTime date, int calories)
            => new CustomDay(planId, date, calories);
    }
}