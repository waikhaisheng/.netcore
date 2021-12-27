using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Filters
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211219
    /// UpdatedBy: 
    /// Updated: 
    /// </summary>
    public class ApiBaseActionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        public ApiBaseActionAttribute()
        {
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

            }
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

    }
}
