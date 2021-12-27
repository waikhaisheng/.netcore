using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Middlewares;

namespace NUnitTestWebApplication.Middlewares
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211226
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class TestErrorHandlerMiddleware
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211226
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        [SetUp]
        public void Setup()
        {

        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211226
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        [Test]
        public async Task TestInvokeAsync()
        {
            var httpContext = new DefaultHttpContext();
            RequestDelegate next = (HttpContext hc) => Task.CompletedTask;
            var errorHandlerMiddleware = new ErrorHandlerMiddleware(next);
            
            await errorHandlerMiddleware.InvokeAsync(httpContext);

        }
    }
}
