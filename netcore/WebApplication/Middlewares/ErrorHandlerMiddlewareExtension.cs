using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Middlewares
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211219
    /// UpdatedBy: Wai Khai Sheng
    /// Updated: 20211227
    /// </summary>
    public static class ErrorHandlerMiddlewareExtension
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
