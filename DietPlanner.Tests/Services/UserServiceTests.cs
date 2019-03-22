using AutoMapper;
using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;
using DietPlanner.Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {

        [Test]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns("somesalt");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hashedpassword");

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);
            await userService.RegisterAsync("user", "user@email.com", "password", "user");
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void register_async_with_existing_email_should_not_invoke_add_async_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();

            userRepositoryMock.Setup(x => x.GetAsync("user@email.com")).ReturnsAsync(() => User.Create("user11","user@email.com", "user", "password","salt"));

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, encrypterMock.Object);
            Assert.ThrowsAsync<Exception>
                (() => userService.RegisterAsync("user11", "user@email.com", "password", "user"));
        }

        [Test]
        public async Task get_async_with_nonexistent_email_should_return_null()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object,encrypterMock.Object);
            var user = await userService.GetAsync("notexist@email.com");
            Assert.AreEqual(user, null);
        }

        [Test]
        public async Task get_plan_async_with_nonexisten_email_should_return_null()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var planService = new PlanService(userRepositoryMock.Object, mapperMock.Object);
            var plan = await planService.GetUserPlanAsync("notexist@email.com");
            Assert.AreEqual(plan, null);
        }

        [Test]
        public async Task register_user_plan_async_should_invoke_add_users_plan_async_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            userRepositoryMock.Setup(x => x.GetAsync("user@email.com")).ReturnsAsync(() => User.Create("user11", "user@email.com", "user", "password", "salt"));

            var planService = new PlanService(userRepositoryMock.Object, mapperMock.Object);
            await planService.RegisterUsersPlanAsync("user@email.com", 60, DateTime.UtcNow);
            userRepositoryMock.Verify(x => x.AddUsersPlanAsync(It.IsAny<User>(),It.IsAny<int>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
