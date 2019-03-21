using DietPlanner.Api;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.TestsEndToEnd.Controllers
{
    public class AccountControllerTests: ControllerTestsBase
    {

        [Test]
        public async Task given_valid_current_and_new_password_it_should_be_changed()
        {
            var command = new ChangeUserPassword
            {
                currentPassword = "supersecret",
                newPassword = "newsecret"
            };
            var payload = GetPayload(command);
            var response = await Client.PutAsync("account/passwordchange", payload);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }
    }
}
