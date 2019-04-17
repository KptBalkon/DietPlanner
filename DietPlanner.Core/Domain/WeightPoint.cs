using DietPlanner.Core.Extensions;
using System;

namespace DietPlanner.Core.Domain
{
    public class WeightPoint
    {
        public Guid WeightPointId { get; protected set; }
        public Guid UserId { get; protected set; }
        public int Weight { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public User User { get; set; }

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
        /// <param name="planId"></param>
        /// <param name="weight"></param>
        /// <param name="weightTime"></param>
        /// <returns></returns>
        public static WeightPoint Create(Guid userId, int weight, DateTime? weightTime = null)
            => new WeightPoint(userId, weight, weightTime);
    }
}