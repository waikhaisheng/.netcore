using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.WebApplicationModels.CommonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Filters;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211219
    /// UpdatedBy: Wai Khai Sheng
    /// Updated: 20211227
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiBaseAction]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly short _cancellationSecond = 10;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly string _httpClientLocal;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public ValuesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpClientLocal = _configuration["ProjectHttpClient:DemowebapplicationTest:Name"];
            _cancellationSecond = Convert.ToInt16(_configuration["UserSetting:CancellationSecond"]);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Action")]
        //[Authorize]
        public IActionResult GetAction()
        {
            var ret = $"Action Get";
            return Ok(ret);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Action/{input}")]
        //[Authorize]
        public IActionResult GetAction(string input)
        {
            var ret = $"Action Get {input}";
            return Ok(ret);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Action")]
        //[Authorize]
        public IActionResult PostAction(ReqestBase input)
        {
            var ret = $"Action Post {input.Id}";
            return Ok(ret);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Action")]
        //[Authorize]
        public IActionResult PutAction(ReqestBase input)
        {
            var ret = $"Action Put {input.Id}"; 
            return Ok(ret);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Action")]
        //[Authorize]
        public IActionResult DeleteAction(ReqestBase input)
        {
            var ret = $"Action Delete {input.Id}";
            return Ok(ret);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("ActionException")]
        //[Authorize]
        public IActionResult GetActionException()
        {
            var a = Convert.ToInt32("a");
            var ret = $"Action Get";
            return Ok(ret);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("Cors")]
        public async Task<IActionResult> CorsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient(_httpClientLocal);
            var url = "/api/ApiGen";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(_cancellationSecond));
            var resRaw = await httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cts.Token);
            var res = await resRaw.Content.ReadAsStringAsync();
            return Ok(res);
        }
    }
}
