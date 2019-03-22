using System.Threading.Tasks;
using DietPlanner.Infrastructure.Commands;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.Services;
using DietPlanner.Infrastructure.Settings;
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
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Created($"users/{command.Email}", new object());
        }


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

        [HttpPost("{email}/plan")]
        public async Task<IActionResult> PostPlan([FromBody]AddUserPlan command, [FromRoute]string email)
        {
            command.email = email;
            await CommandDispatcher.DispatchAsync(command);
            return Created($"users/{command.email}/plan", new object());
        }
    }
}