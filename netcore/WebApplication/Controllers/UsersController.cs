using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.WebApplicationModels.Controllers.User;
using Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Filters;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="reqPayload"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(ReqPayloadDto reqPayload)
        {
            var response = _userService.Authenticate(reqPayload.Payload);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("AuthenticateWithoutEncypt")]
        public IActionResult AuthenticateWithoutEncypt(AuthenticateReq req)
        {
            var response = _userService.Authenticate(req.Username, req.Password);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        [JwtAuthorize]
        [HttpGet("isAuthenticated")]
        public IActionResult IsAuthenticate()
        {
            return Ok(true);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        [JwtAuthorize]
        [HttpGet("AuthenticatedUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult AuthenticatedUser()
        {
            var u = HttpContext.Items["User"];
            return Ok(u);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <returns></returns>
        [JwtAuthorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
