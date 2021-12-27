using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;//Microsoft.Extensions.Configuration.Json
using Database.Users;
using Models.DatabaseModels;

namespace NUnitTestDatabase.Users
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class TestUserDbContext
    {
        private User _user = new User
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Email = $"unittest1@gmail.com",
            Password = $"test123",
            Username = $"unittest1"
        };
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
        private IConfiguration Configuration;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Configuration = InitConfiguration();
        }
        
        [Test]
        public void TestUser()
        {
            TestAddUser();
            TestUpdateUser();
            TestRemoveUser();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestLogin()
        {
            var user = new User
            {
                Id = Guid.Parse("00000000-0000-0000-0001-000000000001"),
                Email = $"test@gmail.com",
                Password = $"test123",
                Username = $"test"
            };
            var cs = Configuration["ConnectionStrings:DefaultConnection"];
            
            var userDbContext = new UserDbContext(cs);
            var ret = userDbContext.Login(user.Email, user.Password);

            Assert.IsNotNull(ret);
        }

        #region private Testcases
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private void TestAddUser()
        {
            var cs = Configuration["ConnectionStrings:DefaultConnection"];

            var userDbContext = new UserDbContext(cs);
            var ret = userDbContext.AddUser(_user);

            Assert.IsNotNull(ret);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private void TestUpdateUser()
        {
            var user = new User
            {
                Id = _user.Id,
                Email = $"unittest1update@gmail.com",
                Password = $"test123update",
                Username = $"unittest1update"
            };
            var cs = Configuration["ConnectionStrings:DefaultConnection"];

            var userDbContext = new UserDbContext(cs);
            var ret = userDbContext.UpdateUser(user);

            Assert.IsNotNull(ret);
            Assert.AreEqual(user.Email, ret.Email);
            Assert.AreEqual(user.Username, ret.Username);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private void TestRemoveUser()
        {
            var cs = Configuration["ConnectionStrings:DefaultConnection"];

            var userDbContext = new UserDbContext(cs);
            var ret = userDbContext.RemoveUser(_user.Id);

            Assert.IsTrue(ret);
        }
        #endregion
    }
}
