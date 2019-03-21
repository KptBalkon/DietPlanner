using DietPlanner.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Services
{
    public interface IUserService: IService
    {
        Task<UserDTO> GetAsync(string email);
        Task RegisterAsync(string username, string email, string password, string role);
        Task LoginAsync(string email, string password);
    }
}
