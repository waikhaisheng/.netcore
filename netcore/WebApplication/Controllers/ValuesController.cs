using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.WebApplicationModels.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Filters;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211219
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiBaseAction]
    public class ValuesController : ControllerBase
    {
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
        [Route("ActionException")]
        //[Authorize]
        public IActionResult GetActionException()
        {
            var a = Convert.ToInt32("a");
            var ret = $"Action Get";
            return Ok(ret);
        }
    }
}
