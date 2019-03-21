using DietPlanner.Core.Extensions;
using System;

namespace DietPlanner.Core.Domain
{
    public class WeightPoint
    {
        Guid WeightPointId { get; set; }
        Guid UserId { get; set; }
        int Weight { get; set; }
        DateTime CreatedAt { get; set; }

        protected WeightPoint()
        {
        }

        protected WeightPoint(Guid userId, int weight, DateTime? weightTime=null)
        {
            WeightPointId = Guid.NewGuid();
            UserId = userId;
            Weight = weight;
            CreatedAt = weightTime.DateSetToUtcNowIfNull();
        }

        /// <summary>
        /// If DateTime is null, we assign current date to it.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="weight"></param>
        /// <param name="weightTime"></param>
        /// <returns></returns>
        public static WeightPoint Create(Guid userId, int weight, DateTime? weightTime = null)
            => new WeightPoint(userId, weight, weightTime);
    }
}