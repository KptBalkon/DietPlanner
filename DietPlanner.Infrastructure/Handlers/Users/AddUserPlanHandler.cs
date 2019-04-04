using DietPlanner.Infrastructure.Commands;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Handlers.Users
{
    public class AddUserPlanHandler : ICommandHandler<AddUserPlan>
    {
        private readonly IPlanService _planService;

        public AddUserPlanHandler(IPlanService planService)
        {
            _planService = planService;
        }

        public async Task HandleAsync(AddUserPlan command)
        {
            await _planService.RegisterUsersPlanAsync(command.Procurer, command.plannedWeight, command.targetDate);
        }
    }
}
