using DietPlanner.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Services
{
    public interface IUserService: IService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetAsync(string email);
        Task<UserDTO> GetAsync(Guid id);
        Task RegisterAsync(Guid userId, string username, string email, string password, string role);
        Task LoginAsync(string email, string password);
        Task UpdateUserAsync(Guid userId, string username, string email, string password, string roles);
        Task DeleteUserAsync(Guid userId);
    }
}
