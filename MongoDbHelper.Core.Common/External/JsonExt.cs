using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace MongoDbHelper.Core.Common.External
{
    public static class JsonExtHelper
    {
        static JsonExtHelper()
        {

        }

        /// <summary>
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            try
            {
                if (null == obj)
                    return null;
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ObjectToJSON<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        public static T JSONToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static object JSONToObject(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }



    }
}
