using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;
using DietPlanner.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Repositories
{
    public class PlanRepository : IPlanRepository, ISqlRepository
    {
        private readonly DietPlannerContext _context;

        public PlanRepository(DietPlannerContext context)
        {
            _context = context;
        }
        public async Task AddPlan(Plan plan)
        {
            _context.Add(plan);
            await _context.SaveChangesAsync();
        }

        public async Task AddCustomDay(CustomDay customDay)
        {
            _context.Add(customDay);
            await _context.SaveChangesAsync();
        }

    }
}
