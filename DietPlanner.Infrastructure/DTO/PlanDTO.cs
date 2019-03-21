using DietPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietPlanner.Infrastructure.DTO
{
    public class PlanDTO
    {
        public Guid PlanId { get; set; }
        public Guid UserId { get; set; }
        public int PlannedWeight { get; set; }
        public DateTime TargetDate { get; set; }
        public ISet<CustomDay> CustomDays;
    }
}
