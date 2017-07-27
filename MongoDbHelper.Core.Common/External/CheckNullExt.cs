using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbHelper.Core.Common.External
{
    public static class NullExt
    {
        /// <summary>
        ///  判断对象不为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// 判断对象为空的
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 判断字符串不为null,且有内容.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }


        /// <summary>
        /// 判断字符串为null,无内容.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断某个IEnumerable<T>不为空且含有元素.
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static bool IsAny<T>(this IEnumerable<T> lst)
        {
            return lst != null && lst.Any();
        }
    }
}
