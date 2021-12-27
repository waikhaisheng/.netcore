using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Filters;

namespace NUnitTestWebApplication.Filters
{
    public class TestJwtAuthorizeAttribute
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [SetUp]
        public void Setup()
        {

        }
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        [Test]
        public void TestOnAuthorization()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            var context = new AuthorizationFilterContext(
                new ActionContext(
                    httpContext: httpContext,
                    routeData: new RouteData(),
                    actionDescriptor: new ActionDescriptor()
                ),
                new List<IFilterMetadata>());
            var jwtAttribute = new JwtAuthorizeAttribute();
            var assertRet = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            //Act
            jwtAttribute.OnAuthorization(context);
            var ret = context.Result as JsonResult;

            //Assert
            Assert.IsNotNull(ret);
            Assert.AreEqual(assertRet.StatusCode, ret.StatusCode);
        }
    }
}
