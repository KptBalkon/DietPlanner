using System;
using System.Threading.Tasks;
using DietPlanner.Infrastructure.Commands;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.Services;
using DietPlanner.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Api.Controllers
{
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPlanService _planService;

        public UsersController(IUserService userService, IPlanService planService,
            ICommandDispatcher commandDispatcher, GeneralSettings settings) : base(commandDispatcher)
        {
            _userService = userService;
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            if (users == null)
            {
                return NotFound();
            }

            return Json(users);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await DispatchAsync(command);
            return Created($"users/{command.Email}", new object());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpGet("{userID:guid}")]
        public async Task<IActionResult> Get(Guid userID)
        {
            var user = await _userService.GetAsync(userID);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Put([FromBody]UpdateUser command, [FromRoute]Guid userId)
        {
            var user = await _userService.GetAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            command.UserId = user.UserId;

            await DispatchAsync(command);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{email}")]
        public async Task<IActionResult> Put([FromBody]UpdateUser command, [FromRoute]string email)
        {
            var user = await _userService.GetAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            command.UserId = user.UserId;

            await DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> PutMe([FromBody]UpdateUser command)
        {
            command.UserId = Guid.Parse(User.Identity.Name);

            if (command.Role == "admin")
            {
                return Unauthorized();
            }

            await DispatchAsync(command);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid userId)
        {
            var user = await _userService.GetAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            DeleteUser command = new DeleteUser { UserId = user.UserId };

            await DispatchAsync(command);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete([FromRoute]string email)
        {
            var user = await _userService.GetAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            DeleteUser command = new DeleteUser { UserId = user.UserId };

            await DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            DeleteUser command = new DeleteUser { UserId = Guid.Parse(User.Identity.Name) };
            await DispatchAsync(command);
            return Ok();
        }


        //DODOK
        [HttpGet("{email}/plan")]
        public async Task<IActionResult> GetPlan(string email)
        {
            var user = await _userService.GetAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var plan = await _planService.GetUserPlanAsync(email);

            return Json(plan);
        }


        //DODOK
        [HttpPost("{email}/plan")]
        public async Task<IActionResult> PostPlan([FromBody]AddUserPlan command, [FromRoute]string email)
        {
            command.email = email;
            await DispatchAsync(command);
            return Created($"users/{command.email}/plan", new object());
        }
    }
}