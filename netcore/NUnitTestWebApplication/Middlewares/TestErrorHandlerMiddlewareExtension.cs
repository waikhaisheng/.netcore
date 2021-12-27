using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Middlewares;

namespace NUnitTestWebApplication.Middlewares
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class TestErrorHandlerMiddlewareExtension
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [SetUp]
        public void Setup()
        {

        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public async Task TestUseErrorHandlerMiddleware()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            //services.AddMyServices();
                        })
                        .Configure(app =>
                        {
                            app.UseErrorHandlerMiddleware();
                        });
                })
                .StartAsync();
            var response = await host.GetTestClient().GetAsync("/");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
