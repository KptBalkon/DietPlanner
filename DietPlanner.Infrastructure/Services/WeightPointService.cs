using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;

namespace DietPlanner.Infrastructure.Services
{
    public class WeightPointService : IWeightPointService
    {
        private readonly IWeightPointRepository _weightPointRepository;

        public WeightPointService(IWeightPointRepository weightPointRepository)
        {
            _weightPointRepository = weightPointRepository;
        }

        public async Task AddWeightPointAsync(Guid userId, DateTime weightDate, int weight)
        {
            WeightPoint weightPoint = WeightPoint.Create(userId, weight, weightDate);
            await _weightPointRepository.AddAsync(weightPoint);
        }

        public Task<IEnumerable<WeightPoint>> GetAllAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveWeightPointAsync(Guid weightPointGuid)
        {
            throw new NotImplementedException();
        }
    }
}
