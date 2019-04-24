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
            if(command.Role=="admin" && !User.HasClaim("role","admin"))
            {
                return Unauthorized();
            }
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

        [Authorize(Roles = "admin")]
        [HttpGet("{email}/plan")]
        public async Task<IActionResult> GetPlan([FromRoute]string email)
        {
            var user = await _userService.GetAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var plan = await _planService.GetUserPlanAsync(user.UserId);

            return Json(plan);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{userId:guid}/plan")]
        public async Task<IActionResult> GetPlan([FromRoute]Guid userId)
        {
            var user = await _userService.GetAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var plan = await _planService.GetUserPlanAsync(user.UserId);

            return Json(plan);
        }

        [Authorize]
        [HttpGet("me/plan")]
        public async Task<IActionResult> GetMyPlan()
        {
            var user = await _userService.GetAsync(UserId);

            var plan = await _planService.GetUserPlanAsync(UserId);

            return Json(plan);
        }

        [Authorize]
        [HttpPost("me/plan")]
        public async Task<IActionResult> PostPlan([FromBody]AddUserPlan command)
        {
            await DispatchAsync(command);
            return Created($"users/me/plan", new object());
        }
        
        [Authorize]
        [HttpPost("me/plan/customday")]
        public async Task<IActionResult> PostCustomDay([FromBody]AddCustomDay command)
        {
            await DispatchAsync(command);
            return Created($"users/me/plan", new object());
        }

        [Authorize]
        [HttpPost("me/weight")]
        public async Task<IActionResult> PostWeightPoint([FromBody]AddWeightPoint command)
        {
            await DispatchAsync(command);
            return Created($"users/me", new object());
        }
        /// <summary>
        /// Calculates calories for each day from now till planned target date basing on user's plan and custom calorie days.
        /// </summary>
        [Authorize]
        [HttpPost("me/plan/calculate")]
        public async Task<IActionResult> GetCalculatedPlan([FromBody]CalculatePlan command)
        {
            await DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpPut("me/details")]
        public async Task<IActionResult> PutUserDetails([FromBody]PutUserDetails command)
        {
            await DispatchAsync(command);
            return Ok();
        }
    }
}