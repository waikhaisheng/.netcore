using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Middlewares
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211219
    /// UpdatedBy: Wai Khai Sheng
    /// Updated: 20211227
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
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
            jsonString = JsonConvert.SerializeObject(exception);
            return GenerateResponse(context, jsonString);
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
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
