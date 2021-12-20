using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace CommonLib.TestLib
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211219
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public sealed class TestServerWebHost : IDisposable
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly TestServer _testServer;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        public HttpClient _httpClient { get; }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
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
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }
    }
}
