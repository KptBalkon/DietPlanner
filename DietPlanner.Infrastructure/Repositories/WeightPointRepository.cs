using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;
using DietPlanner.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Repositories
{
    public class WeightPointRepository: IWeightPointRepository
    {
        private readonly DietPlannerContext _context;

        public WeightPointRepository(DietPlannerContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WeightPoint weightPoint)
        {
            await _context.WeightPoints.AddAsync(weightPoint);
            await _context.SaveChangesAsync();
        }
    }
}
