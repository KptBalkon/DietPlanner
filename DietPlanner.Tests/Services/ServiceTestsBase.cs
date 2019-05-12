using DietPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DietPlanner.Tests.Services
{
    public class ServiceTestsBase
    {
        static public Guid nonexistentGuid;
        static public Guid testUserGuid;
        static public User testUser;
        static public User robustTestUser;

        public ServiceTestsBase()
        {
            nonexistentGuid = new Guid("12345678-bead-babe-dead-badbadbadbad");
            testUserGuid = new Guid("3103DF88-C511-4718-8A26-51663C33231A");
            testUser = User.Create(testUserGuid, "testuser", "test@email.com", "user", "testtest", "salt");
            robustTestUser = testUser;
            robustTestUser.Update(null, null, null, null, null, "192", "male", Convert.ToDateTime("11-11-1992"));
        }
    }
}