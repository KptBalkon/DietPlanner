using DietPlanner.Api;
using DietPlanner.Infrastructure.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.TestsEndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        public ControllerTestsBase()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected async Task<UserDTO> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDTO>(responseString);
        }
    }
}
