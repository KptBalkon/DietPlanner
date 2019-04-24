using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Handlers
{
    public class CalculatePlanHandler
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IPlanCalculatorService _planCalculatorService;

        public CalculatePlanHandler(IMemoryCache memoryCache, IPlanCalculatorService planCalculatorService)
        {
            _memoryCache = memoryCache;
            _planCalculatorService = planCalculatorService;
        }

        public async Task HandleAsync(CalculatePlan command)
        {
            //TODO:HandleAsync


            //await _userService.LoginAsync(command.Email, command.Password);
            //var user = await _userService.GetAsync(command.Email);
            //var jwt = _jwtHandler.CreateToken(user.UserId, user.Role);
            //_memoryCache.SetJwt(command.TokenId, jwt);
        }

    }
}
