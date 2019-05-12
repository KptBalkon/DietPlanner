using AutoMapper;
using DietPlanner.Core.Domain;
using DietPlanner.Core.Repositories;
using DietPlanner.Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Tests.Services
{
    [TestFixture]
    public class PlanServiceTests : ServiceTestsBase
    {

        [Test]
        public async Task calculating_plan_for_nonexistent_user_should_return_exception()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var planRepositoryMock = new Mock<IPlanRepository>();
            var mapperMock = new Mock<IMapper>();

            userRepositoryMock.Setup(x => x.GetAsync(nonexistentGuid)).ReturnsAsync(() => null);

            var planService = new PlanService(userRepositoryMock.Object, planRepositoryMock.Object, mapperMock.Object);
            
            Assert.ThrowsAsync<Exception>
                (() => planService.CalculatePlan(nonexistentGuid, 3));

        }

        [Test]
        public async Task calculating_plan_for_user_without_plan_should_return_exception()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var planRepositoryMock = new Mock<IPlanRepository>();
            var mapperMock = new Mock<IMapper>();

            userRepositoryMock.Setup(x => x.GetAsync(testUserGuid)).ReturnsAsync(() => robustTestUser);

            var planService = new PlanService(userRepositoryMock.Object, planRepositoryMock.Object, mapperMock.Object);

            Assert.ThrowsAsync<Exception>
                (() => planService.CalculatePlan(testUserGuid, 3));

        }

        [Test]
        public async Task calculating_plan_for_user_without_weightpoints_should_return_exception()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var planRepositoryMock = new Mock<IPlanRepository>();
            var mapperMock = new Mock<IMapper>();

            userRepositoryMock.Setup(x => x.GetAsync(testUserGuid)).ReturnsAsync(() => robustTestUser);

            var planService = new PlanService(userRepositoryMock.Object, planRepositoryMock.Object, mapperMock.Object);

            Assert.ThrowsAsync<Exception>
                (() => planService.CalculatePlan(testUserGuid, 3));

        }
    }
}