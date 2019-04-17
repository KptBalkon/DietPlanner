using DietPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Services
{
    public interface IWeightPointService: IService
    {
        Task<IEnumerable<WeightPoint>> GetAllAsync(Guid userId);
        Task AddWeightPointAsync(Guid userId, DateTime weightDate, int weight);
        Task RemoveWeightPointAsync(Guid weightPointGuid);
    }
}
