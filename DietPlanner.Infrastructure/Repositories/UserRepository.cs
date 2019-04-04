using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;
using DietPlanner.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, ISqlRepository
    {
        private readonly DietPlannerContext _context;

        public UserRepository(DietPlannerContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(Guid userId)
        {
           return await _context.Users.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> GetAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await GetAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddUsersPlanAsync(Plan plan)
        {
            _context.Add(plan);
                await _context.SaveChangesAsync();
        }
    }
}
