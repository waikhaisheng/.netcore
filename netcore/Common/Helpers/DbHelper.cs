using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    /// <summary>
    /// Creater: Wai Khai Sheng
    /// Created: 20211227
    /// Updated: 
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy:
        /// Updated: 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ObjectOrDefaultDBNull<T>(this object obj)
        {
            if (obj == System.DBNull.Value)
                return default(T);

            return (T)obj;
        }
    }
}
