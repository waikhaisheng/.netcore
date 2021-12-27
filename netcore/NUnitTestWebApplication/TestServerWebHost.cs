using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestWebApplication
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211219
    /// UpdatedBy: Wai Khai Sheng
    /// Updated: 20211227
    /// </summary>
    public sealed class TestServerWebHost : IDisposable
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        private readonly TestServer _testServer;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        public HttpClient _httpClient { get; }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        /// <param name="webHostBuilder"></param>
        public TestServerWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseEnvironment(Environments.Development);
            webHostBuilder.UseKestrel();
            _testServer = new TestServer(webHostBuilder);
            _httpClient = _testServer.CreateClient();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }
    }
}
