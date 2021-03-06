﻿using DietPlanner.Api;
using DietPlanner.Infrastructure.Commands.Users;
using DietPlanner.Infrastructure.DTO;
using DietPlanner.TestsEndToEnd.DTO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.TestsEndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        static public Guid nonexistentGuid;
        static public Guid testUserGuid;
        
        public ControllerTestsBase()
        {
            Server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            Client = Server.CreateClient();
            nonexistentGuid = new Guid("12345678-bead-babe-dead-badbadbadbad");
            testUserGuid = new Guid("3103DF88-C511-4718-8A26-51663C33231A");
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

        protected async Task<UserDTO> GetUserAsync(Guid guid)
        {
            var response = await Client.GetAsync($"users/{guid}");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDTO>(responseString);
        }

        //TODO: Remove this method after testing
        protected async Task<TokenDTO> GetRandomTokenAsync()
        {
            var response = await Client.GetAsync("account/usertoken");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenDTO>(responseString);
        }

        protected async Task<ExceptionDTO> ConvertResponseToExceptionDTOAsync(HttpResponseMessage httpResponseMessage)
        {
            var stringresponse = await httpResponseMessage.Content.ReadAsStringAsync();
            ExceptionDTO exception = JsonConvert.DeserializeObject<ExceptionDTO>(stringresponse);
            exception.StatusCode = httpResponseMessage.StatusCode;
            return exception;
        }

        protected async Task<string> Login(string userEmail, string password)
        {
            var request = new Login
            {
                Email = userEmail,
                Password = password
            };
            var payload = GetPayload(request);
            var response = await Client.PostAsync("login", payload);

            var stringresponse = await response.Content.ReadAsStringAsync();
            TokenDTO token = JsonConvert.DeserializeObject<TokenDTO>(stringresponse);
            return token.Token;
        }


    }
}
