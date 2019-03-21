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
    public class UsersControllerTests: ControllerTestsBase
    {

        [Test]
        public async Task given_valid_email_user_should_exist()
        {
            //Act
            var email = "empire@smail.com";
            var user = await GetUserAsync(email);

            //Assert
            Assert.AreEqual(user.Email, email);
        }

        [Test]
        public async Task given_invalid_email_user_should_not_exist()
        {
            //Act
            var email = "empiresrempire@smail.com";
            var response = await Client.GetAsync($"users/{email}");

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task given_unique_email_user_should_be_created()
        {
            var request = new CreateUser
            {
                Email = "newuser@email.com",
                Username = "TestMike",
                Password = "secretpassword"
            };
            var payload = GetPayload(request);
            var response = await Client.PostAsync("users", payload);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.AreEqual(response.Headers.Location.ToString(), $"users/{request.Email}");

            var user = await GetUserAsync(request.Email);
            Assert.AreEqual(user.Email, request.Email);
        }
        
        [Test]
        public async Task given_inproper_email_exception_should_be_thrown()
        {
            var request = new CreateUser
            {
                Email = "itsnotanemaildude",
                Username = "User",
                Password = "password"
            };
            var payload = GetPayload(request);
            var ex = Assert.ThrowsAsync<Exception>(async() => await Client.PostAsync("users", payload));
            Assert.That(ex.Message, Is.EqualTo("Please provide properly formatted Email."));
            await Task.CompletedTask;
        }

        [Test]
        public async Task given_too_short_username_exception_should_be_thrown()
        {
            var request = new CreateUser
            {
                Email = "Userro@email.com",
                Username = "X",
                Password = "password"
            };
            var payload = GetPayload(request);
            var ex = Assert.ThrowsAsync<Exception>(async () => await Client.PostAsync("users", payload));
            Assert.That(ex.Message, Is.EqualTo("Username must be longer than 3 characters."));
            await Task.CompletedTask;
        }

        [Test]
        public async Task given_too_long_username_exception_should_be_thrown()
        {
            var request = new CreateUser
            {
                Email = "Userro@email.com",
                Username = "onehundredonehundredonehundredonehundredonehundredonehundredonehundredonehundredonehundredonehundredoops",
                Password = "password"
            };
            var payload = GetPayload(request);
            var ex = Assert.ThrowsAsync<Exception>(async () => await Client.PostAsync("users", payload));
            Assert.That(ex.Message, Is.EqualTo("Username cannot be longer than 100 characters."));
            await Task.CompletedTask;
        }

        [Test]
        public async Task given_username_with_special_characters_exception_should_be_thrown()
        {
            var request = new CreateUser
            {
                Email = "Userro@email.com",
                Username = "__!BobbyBoy!__",
                Password = "password"
            };
            var payload = GetPayload(request);
            var ex = Assert.ThrowsAsync<Exception>(async () => await Client.PostAsync("users", payload));
            Assert.That(ex.Message, Is.EqualTo("Username can only contain letters and numbers."));
            await Task.CompletedTask;
        }

        [Test]
        public async Task given_too_short_password_exception_should_be_thrown()
        {
            var request = new CreateUser
            {
                Email = "Userro@email.com",
                Username = "BobbyBoy",
                Password = "pas"
            };
            var payload = GetPayload(request);
            var ex = Assert.ThrowsAsync<Exception>(async () => await Client.PostAsync("users", payload));
            Assert.That(ex.Message, Is.EqualTo("Password must be at least 8 characters long"));
            await Task.CompletedTask;
        }

        [Test]
        public async Task given_negative_weight_exception_should_be_thrown()
        {
            var request = new AddUserPlan
            {
                email = "empire@smail.com",
                plannedWeight = -40,
                targetDate = DateTime.Parse("02/02/2020")
            };
            var payload = GetPayload(request);
            var ex = Assert.ThrowsAsync<Exception>(async () => await Client.PostAsync("users/empire@smail.com/plan", payload));
            Assert.That(ex.Message, Is.EqualTo("You cannot plan to have negative weight"));
            await Task.CompletedTask;
        }

        [Test]
        public async Task given_past_target_date_exception_should_be_thrown()
        {
            var request = new AddUserPlan
            {
                email = "empire@smail.com",
                plannedWeight = 80,
                targetDate = DateTime.Parse("02/02/1991")
            };
            var payload = GetPayload(request);
            var ex = Assert.ThrowsAsync<Exception>(async () => await Client.PostAsync("users/empire@smail.com/plan", payload));
            Assert.That(ex.Message, Is.EqualTo("Please provide future date"));
            await Task.CompletedTask;
        }
    }
}
