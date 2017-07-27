using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MongoDbHelper.Core.Common.External
{
    public static class HttpExt
    {
        /// <summary>
        /// Post将对象转为Json格式发送到指定Url
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static async Task<string> PostAsJsonAsync(this object obj, string requestUrl)
        {
            HttpClient client = new HttpClient();
            string responseString = string.Empty;
            HttpContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(requestUrl, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString;
        }

        /// <summary>
        /// Post将对象转为Json格式发送到指定Url
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static async Task<string> PostAsJsonAsync(this object obj, string requestUrl, IEnumerable<KeyValuePair<string, string>> headers)
        {
            HttpClient client = new HttpClient();
            string responseString = string.Empty;
            HttpContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value))
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
            }

            HttpResponseMessage response = await client.PostAsync(requestUrl, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString;
        }

        /// <summary>
        /// Put将对象转为Json格式发送到指定Url
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public static async Task<string> PutAsJsonAsync(this object obj, string requestUrl, IEnumerable<KeyValuePair<string, string>> headers)
        {
            HttpClient client = new HttpClient();
            string responseString = string.Empty;
            HttpContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value))
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
            }

            HttpResponseMessage response = await client.PutAsync(requestUrl, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString;
        }
    }
}
