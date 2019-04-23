using DietPlanner.Infrastructure.Commands;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Handlers.Users
{
    public class PutUserDetailsHandler : ICommandHandler<PutUserDetails>
    {
        private readonly IUserService _userService;

        public PutUserDetailsHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(PutUserDetails command)
        {
            await _userService.UpdateUserDetailsAsync(command.Procurer, command.birthday, command.height, command.sex);
        }
    }
}
