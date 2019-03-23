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
        }

        //These api endpoints are for authorization testing purposes only

        [HttpGet]
        [Route("usertoken")]
        public IActionResult GetToken()
        {
            var token = _jwtHandler.CreateToken(Guid.NewGuid(), "user");

            return Json(token);
        }

        [HttpGet]
        [Route("admintoken")]
        public IActionResult GetAdminToken()
        {
            var token = _jwtHandler.CreateToken(Guid.NewGuid(), "admin");

            return Json(token);
        }

        [HttpGet]
        [Authorize("admin")]
        [Route("auth")]
        public IActionResult GetAuth()
        {
            return Content("You are Authorized");
        }
    }
}