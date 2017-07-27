using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using TCBase.Component;


namespace MongoDbHelper.Core
{
    public class MongodbConfig : Singleton<MongodbConfig>
    {

        public string ServerIP
        {
            get; set;
        } = string.Empty;
        public string Credentials
        {
            get; set;
        } = string.Empty;
        public string ConnMode
        {
            get; set;
        } = string.Empty;
    }
}
