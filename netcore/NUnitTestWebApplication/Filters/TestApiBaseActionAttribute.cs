using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Filters;
using Microsoft.AspNetCore.Routing;

namespace NUnitTestWebApplication.Filters
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211226
    /// UpdatedBy: Wai Khai Sheng
    /// Updated: 20211227
    /// </summary>
    public class TestApiBaseActionAttribute
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211226
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        [SetUp]
        public void Setup()
        {

        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211226
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        [Test]
        public void TestOnActionExecuting()
        {
            //Arrange
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("", "error");
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext(
                    httpContext: httpContext,
                    routeData: new RouteData(),
                    actionDescriptor: new ActionDescriptor(),
                    modelState: modelState
                ),
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            var ret = new ApiBaseActionAttribute();

            //Act
            ret.OnActionExecuting(context);

            //Assert
        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211226
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        [Test]
        public void TestOnActionExecuted()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutedContext(
                new ActionContext(
                    httpContext: httpContext,
                    routeData: new RouteData(),
                    actionDescriptor: new ActionDescriptor()
                ),
                new List<IFilterMetadata>(),
                new Mock<Controller>().Object);

            var ret = new ApiBaseActionAttribute();

            //Act
            ret.OnActionExecuted(context);

            //Assert
        }
    }
}
