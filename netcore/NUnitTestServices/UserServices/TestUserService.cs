using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.UserServices.Interfaces;
using Services.UserServices;
using Models.Dtos;
using Models.Enums;
using Models.Common;
using Common.Utils;
using Models.Constant;
using Newtonsoft.Json;
using Database.Users.Interfaces;
using Database.Users;

namespace NUnitTestServices.UserServices
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class TestUserService
    {
        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
        private IConfiguration Configuration;
        private ServiceProvider serviceProvider;
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
            var services = new ServiceCollection();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDbContext, UserDbContext>(x => new UserDbContext(Configuration.GetConnectionString("DefaultConnection")));
            serviceProvider = services.BuildServiceProvider();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestAuthenticate_AuthenticateRequest()
        {
            var enUsername = CryptographyUtil.EncryptStringAES(AesConstant.AESKEY, AesConstant.AESIV, "test@gmail.com");
            var enPassword = CryptographyUtil.EncryptStringAES(AesConstant.AESKEY, AesConstant.AESIV, "Te3t1234");
            var authenticateRequest = new AuthenticateRequest
            {
                Username = enUsername,
                Password = enPassword
            };
            var srv = serviceProvider.GetRequiredService<IUserService>();

            var ret = srv.Authenticate(authenticateRequest);

            Assert.IsNotNull(ret.Token);
            Assert.IsTrue(ret.Token.Length > 0);
            Assert.AreEqual(AuthenticateEnum.Login, ret.AuthenticateStatus);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestAuthenticate_Payload()
        {
            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test@gmail.com",
                Password = "Te3t1234"
            };
            var payloadStr = JsonConvert.SerializeObject(authenticateRequest);
            var payload = CryptographyUtil.EncryptStringAES(AesConstant.AESKEY, AesConstant.AESIV, payloadStr);
            var srv = serviceProvider.GetRequiredService<IUserService>();

            var ret = srv.Authenticate(payload);

            Assert.IsNotNull(ret.Token);
            Assert.IsTrue(ret.Token.Length > 0);
            Assert.AreEqual(AuthenticateEnum.Login, ret.AuthenticateStatus);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestAuthenticate_UsernamePassword()
        {
            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test@gmail.com",
                Password = "test123"
            };
            var srv = serviceProvider.GetRequiredService<IUserService>();

            var ret = srv.Authenticate(authenticateRequest.Username, authenticateRequest.Password);

            Assert.IsNotNull(ret.Token);
            Assert.IsTrue(ret.Token.Length > 0);
            Assert.AreEqual(AuthenticateEnum.Login, ret.AuthenticateStatus);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestGetAll()
        {
            var srv = serviceProvider.GetRequiredService<IUserService>();

            var ret = srv.GetAll();

            Assert.IsNotNull(ret);
        }
        [Test]
        public void TestLogout()
        {
            var srv = serviceProvider.GetRequiredService<IUserService>();

            var ex = Assert.Throws<NotImplementedException>(() => { srv.Logout(Guid.Empty).GetAwaiter().GetResult(); });

            Assert.IsNotNull(ex);
        }
    }
}
