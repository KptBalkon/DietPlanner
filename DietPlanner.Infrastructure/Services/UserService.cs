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
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IMapper mapper, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetAsync(string email)
        {
            User user = await _userRepository.GetAsync(email);

            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task<UserDTO> GetAsync(Guid id)
        {
            User user = await _userRepository.GetAsync(id);

            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }
            var hash = _encrypter.GetHash(password, user.Salt);
            if(user.Password == hash)
            {
                return;
            }
            throw new Exception("Invalid credentials");
        }

        public async Task RegisterAsync(Guid userId, string username, string email, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);

            if (password.Length < 8)
            {
                throw new Exception("Password must be at least 8 characters long");
            }

            if (user!=null)
            {
                throw new Exception("User with '{email}' already exists");
            }

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);
            user = User.Create(userId, username, email, role, hash, salt);
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(Guid userId, string username, string email, string password, string role)
        {
            var user = await _userRepository.GetAsync(userId);
            string salt = null;

            if (password!=null)
            {
                if(password.Length<8)
                {
                    throw new Exception("Password must be at least 8 characters long");
                }
                salt = _encrypter.GetSalt(password);
                var hash = _encrypter.GetHash(password, salt);
            }
            var changedUser = User.Create(userId, username ?? user.Username, email?? user.Email, role?? user.Role, password?? user.Password, salt?? user.Salt);
            await _userRepository.UpdateAsync(changedUser);
        }
    }
}
