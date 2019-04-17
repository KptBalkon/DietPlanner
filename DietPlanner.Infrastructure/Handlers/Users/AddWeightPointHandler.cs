using DietPlanner.Infrastructure.Commands;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Infrastructure.Handlers.Users
{
    public class AddWeightPointHandler : ICommandHandler<AddWeightPoint>
    {
        private readonly IWeightPointService _weightPointService;

        public AddWeightPointHandler(IWeightPointService weightPointService)
        {
            _weightPointService = weightPointService;
        }

        public async Task HandleAsync(AddWeightPoint command)
        {
            await _weightPointService.AddWeightPointAsync(command.Procurer, command.WeightDate, command.Weight);
        }
    }
}
