using DietPlanner.Core.Extensions;
using System;

namespace DietPlanner.Core.Domain
{
    public class WeightPoint
    {
        public Guid WeightPointId { get; protected set; }
        public Guid PlanId { get; protected set; }
        public int Weight { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected WeightPoint()
        {
        }

        protected WeightPoint(Guid planId, int weight, DateTime? weightTime=null)
        {
            WeightPointId = Guid.NewGuid();
            PlanId = planId;
            Weight = weight;
            CreatedAt = weightTime.DateSetToUtcNowIfNull();
        }

        /// <summary>
        /// If DateTime is null, we assign current date to it.
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="weight"></param>
        /// <param name="weightTime"></param>
        /// <returns></returns>
        public static WeightPoint Create(Guid planId, int weight, DateTime? weightTime = null)
            => new WeightPoint(planId, weight, weightTime);
    }
}