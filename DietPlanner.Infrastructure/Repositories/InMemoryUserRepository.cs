using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;
using DietPlanner.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>();


        public async Task<User> GetAsync(Guid userId)
        {
            return await Task.FromResult(_users.SingleOrDefault(x => x.UserId == userId));
        }

        public async Task<User> GetAsync(string email)
        {
            return await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(_users);
        }

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid UserId)
        {
            var user = await GetAsync(UserId);
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            var userToRemove = await GetAsync(user.UserId);
            _users.Remove(userToRemove);
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task AddUsersPlanAsync(User user, int plannedWeight, DateTime targetDate)
        {
            user.CreatePlan(plannedWeight, targetDate);
            await Task.CompletedTask;
        }

        public Task UpdateDetailsAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
