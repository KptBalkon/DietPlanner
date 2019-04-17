using DietPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Core.Repositories
{
    public interface IWeightPointRepository: IRepository
    {
        Task AddAsync(WeightPoint weightPoint);

    }
}
