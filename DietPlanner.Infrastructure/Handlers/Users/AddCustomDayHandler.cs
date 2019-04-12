using DietPlanner.Infrastructure.Commands;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Handlers.Users
{
    public class AddCustomDayHandler: ICommandHandler<AddCustomDay>
    {
        private readonly IPlanService _planService;

        public AddCustomDayHandler(IPlanService planService)
        {
            _planService = planService;
        }

        public async Task HandleAsync(AddCustomDay command)
        {
            await _planService.AddCustomDayAsync(command.Procurer, command.date, command.calories);
        }
    }
}
