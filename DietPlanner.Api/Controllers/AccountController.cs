using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Infrastructure.Commands;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Api.Controllers
{

    public class AccountController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;

        public AccountController(IUserService userService, ICommandDispatcher commandDispatcher, 
            IJwtHandler jwtHandler) : base(commandDispatcher)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            Console.WriteLine(_jwtHandler.CreateToken("any@mail.com","user"));
        }

        [HttpPut]
        [Route("passwordchange")]
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }

        [HttpGet]
        [Route("token")]
        public IActionResult Get()
        {
            var token = _jwtHandler.CreateToken("user1@email.com", "user");

            return Json(token);
        }


        [HttpGet]
        [Authorize]
        [Route("auth")]
        public IActionResult GetAuth()
        {
            return Content("You are Authorized");
        }
    }
}