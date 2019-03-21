using AutoMapper;
using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;
using DietPlanner.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
       
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetAsync(string email)
        {
            User user = await _userRepository.GetAsync(email);

            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task RegisterAsync(string username, string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if(user!=null)
            {
                throw new Exception("User with '{email}' already exists");
            }

            var salt = Guid.NewGuid().ToString("N"); //TEMP - Still Temporary
            user = User.Create(username, email, password, salt);
            await _userRepository.AddAsync(user);
        }
    }
}
