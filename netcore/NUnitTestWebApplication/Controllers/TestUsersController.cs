using Common.Utils;
using Database.Users;
using Database.Users.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Common;
using Models.Constant;
using Models.Dtos;
using Newtonsoft.Json;
using NUnit.Framework;
using Services.UserServices;
using Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApplication;

namespace NUnitTestWebApplication.Controllers
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class TestUsersController
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
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private ServiceProvider serviceProvider;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
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
        [SetUp]
        public void Setup()
        {
            Configuration = InitConfiguration();
            var webHost = Program.CreateHostBuilder(new string[] { });
            _testServerWebHost = new TestServerWebHost(webHost);
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
        public void TestAuthenticate_ReqPayloadDto()
        {
            // arrange
            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test@gmail.com",
                Password = "Te3t1234"
            };
            var payloadStr = JsonConvert.SerializeObject(authenticateRequest);
            var payload = CryptographyUtil.EncryptStringAES(AesConstant.AESKEY, AesConstant.AESIV, payloadStr);
            var requestParameter = new ReqPayloadDto()
            {
                Payload = payload
            };
            var httpMethod = HttpMethod.Post;
            var httpPath = "/api/Users/authenticate";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);
            request.Content = new StringContent(JsonConvert.SerializeObject(requestParameter), System.Text.Encoding.UTF8, "application/json");

            // act
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            //assert
            Assert.IsNotNull(responseContent);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestAuthenticateWithoutEncypt_AuthenticateReq()
        {
            // arrange
            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test@gmail.com",
                Password = "Te3t1234"
            };
            var httpMethod = HttpMethod.Post;
            var httpPath = "/api/Users/AuthenticateWithoutEncypt";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);
            request.Content = new StringContent(JsonConvert.SerializeObject(authenticateRequest), System.Text.Encoding.UTF8, "application/json");

            // act
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            //assert
            Assert.IsNotNull(responseContent);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestIsAuthenticate()
        {
            // arrange
            var httpMethod = HttpMethod.Get;
            var httpPath = "/api/Users/isAuthenticated";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);
            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test@gmail.com",
                Password = "Te3t1234"
            };
            var srv = serviceProvider.GetRequiredService<IUserService>();
            var jwt = srv.Authenticate(authenticateRequest.Username, authenticateRequest.Password);

            // act
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var ret = JsonConvert.DeserializeObject(responseContent);
            //assert
            Assert.IsNotNull(responseContent);
            Assert.AreEqual(true, ret);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestAuthenticateUser()
        {
            // arrange
            var httpMethod = HttpMethod.Get;
            var httpPath = "/api/Users/AuthenticatedUser";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);
            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test@gmail.com",
                Password = "test123"
            };
            var srv = serviceProvider.GetRequiredService<IUserService>();
            var jwt = srv.Authenticate(authenticateRequest.Username, authenticateRequest.Password);

            // act
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var ret = JsonConvert.DeserializeObject<Guid>(responseContent);
            
            //assert
            Assert.IsNotNull(responseContent);
            Assert.AreEqual(Guid.Parse("00000000-0000-0000-0001-000000000001"), ret);
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
            // arrange
            var httpMethod = HttpMethod.Get;
            var httpPath = "/api/Users";
            HttpClient httpClient = _testServerWebHost._httpClient;
            var request = new HttpRequestMessage(httpMethod, httpPath);
            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test@gmail.com",
                Password = "Te3t1234"
            };
            var srv = serviceProvider.GetRequiredService<IUserService>();
            var jwt = srv.Authenticate(authenticateRequest.Username, authenticateRequest.Password);

            // act
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Token);
            var result = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var responseStream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            var sr = new StreamReader(responseStream);
            var responseContent = sr.ReadToEndAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            
            //assert
            Assert.IsNotNull(responseContent);
        }
    }
}
