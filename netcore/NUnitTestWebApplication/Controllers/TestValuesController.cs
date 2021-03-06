using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using WebApplication;
using Microsoft.AspNetCore.Hosting;
using Models.WebApplicationModels.CommonModels;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using WebApplication.Controllers;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NUnitTestWebApplication.Controllers
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211219
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class TestValuesController
    {
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
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private IConfiguration Configuration;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private TestServerWebHost _testServerWebHost;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private ServiceProvider serviceProvider;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Configuration = InitConfiguration();
            var webHost = Program.CreateHostBuilder(new string[] { });
            _testServerWebHost = new TestServerWebHost(webHost);

            var services = new ServiceCollection();
            //services.AddHttpClient(Configuration["ProjectHttpClient:DemowebapplicationTest:Name"], c =>
            //{
            //    c.BaseAddress = new Uri(Configuration["ProjectHttpClient:DemowebapplicationTest:BaseAddress"]);
            //    c.DefaultRequestHeaders.Add("Accept", "application/json");
            //    c.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            //    c.Timeout = TimeSpan.FromSeconds(15);

            //}).ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler()
            //{
            //    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            //}).SetHandlerLifetime(TimeSpan.FromMinutes(10));
            serviceProvider = services.BuildServiceProvider();
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestGetAction()
        {
            // arrange
            var httpMethod = HttpMethod.Get;
            var httpPath = "/api/Values/Action";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);

            // act
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            // assert
            Assert.AreEqual("Action Get", responseContent);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestGetAction_Input()
        {
            // arrange
            var input = "test123";
            var httpMethod = HttpMethod.Get;
            var httpPath = $"/api/Values/Action/{input}";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);

            // act
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            // assert
            Assert.AreEqual($"Action Get {input}", responseContent);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestPostAction()
        {
            // arrange
            var requestParameter = new ReqestBase()
            {
                Id = Guid.NewGuid(),
                ReqDateTime = DateTime.Now
            };
            var httpMethod = HttpMethod.Post;
            var httpPath = "/api/Values/Action";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);
            request.Content = new StringContent(JsonConvert.SerializeObject(requestParameter), System.Text.Encoding.UTF8, "application/json");

            // act
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            // assert
            Assert.AreEqual($"Action Post {requestParameter.Id}", responseContent);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestPutAction()
        {
            // arrange
            var requestParameter = new ReqestBase()
            {
                Id = Guid.NewGuid(),
                ReqDateTime = DateTime.Now
            };
            var httpMethod = HttpMethod.Put;
            var httpPath = "/api/Values/Action";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);
            request.Content = new StringContent(JsonConvert.SerializeObject(requestParameter), System.Text.Encoding.UTF8, "application/json");

            // act
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            // assert
            Assert.AreEqual($"Action Put {requestParameter.Id}", responseContent);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestDeleteAction()
        {
            // arrange
            var requestParameter = new ReqestBase()
            {
                Id = Guid.NewGuid(),
                ReqDateTime = DateTime.Now
            };
            var httpMethod = HttpMethod.Delete;
            var httpPath = "/api/Values/Action";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);
            request.Content = new StringContent(JsonConvert.SerializeObject(requestParameter), System.Text.Encoding.UTF8, "application/json");

            // act
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            // assert
            Assert.AreEqual($"Action Delete {requestParameter.Id}", responseContent);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestCorsAsync()
        {
            // arrange
            var httpMethod = HttpMethod.Get;
            var httpPath = "/api/Values/Cors";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);

            // act
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var ret = JsonConvert.DeserializeObject(responseContent);

            // assert
            Assert.AreEqual($"test get ok.", ret);
        }
    }
}
