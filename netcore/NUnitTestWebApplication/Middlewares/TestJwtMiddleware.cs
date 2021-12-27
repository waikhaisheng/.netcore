using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Models.Common;
using NUnit.Framework;
using Services.UserServices;
using Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Middlewares;
using Moq;
using Newtonsoft.Json;
using Database.Users.Interfaces;
using Database.Users;

namespace NUnitTestWebApplication.Middlewares
{
    public class TestJwtMiddleware
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
            services.AddScoped<IUserDbContext, UserDbContext>(x => new UserDbContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserService, UserService>();
            serviceProvider = services.BuildServiceProvider();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public async Task TestInvoke()
        {
            var httpContext = new DefaultHttpContext();
            RequestDelegate next = (HttpContext hc) => Task.CompletedTask;
            var mockOption = new Mock<Microsoft.Extensions.Options.IOptions<AppSettings>>();
            AppSettings appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            mockOption.Setup(p => p.Value).Returns(appSettings);
            var errorHandlerMiddleware = new JwtMiddleware(next, mockOption.Object);

            var srv = serviceProvider.GetRequiredService<IUserService>();
            await errorHandlerMiddleware.Invoke(httpContext, srv);
        }
    }
}
