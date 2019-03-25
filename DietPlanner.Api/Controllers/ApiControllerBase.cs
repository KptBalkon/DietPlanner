using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DietPlanner.Infrastructure.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) :
            Guid.Empty;

        protected bool UserIsAdmin => User.HasClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "admin") ?
            true :
            false;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T: ICommand
        {
            if(command is IAuthenticatedCommand authenticatedCommand)
            {
                authenticatedCommand.Procurer = UserId;
            }
            await _commandDispatcher.DispatchAsync(command);
        }
    }
}