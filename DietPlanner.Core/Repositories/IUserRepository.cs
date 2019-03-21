using DietPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Core.Repositories
{
    public interface IUserRepository: IRepository
    {
        Task<User> GetAsync(Guid UserId);
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(Guid UserId);
        Task AddUsersPlanAsync(User user, int plannedWeight, DateTime targetDate);
    }
}
