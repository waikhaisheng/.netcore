using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Common;
using Models.WebApplicationModels.CommonModels;
using Newtonsoft.Json;
using Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Middlewares
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class JwtMiddleware
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly AppSettings _appSettings;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="appSettings"></param>
        public JwtMiddleware(
            RequestDelegate next,
            IOptions<AppSettings> appSettings
        )
        {
            _next = next;
            _appSettings = appSettings.Value;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var isAttachUserToContext = token != null;
            if (isAttachUserToContext)
                await attachUserToContext(context, userService, token);

            await _next(context);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                context.Items["User"] = userId;
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleException(HttpContext context, Exception exception)
        {
            string jsonString = string.Empty;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            jsonString = JsonConvert.SerializeObject(new Exception("Invalid Token"));
            return GenerateResponse(context, jsonString);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        private async Task GenerateResponse(HttpContext context, string jsonString)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }
    }
}
