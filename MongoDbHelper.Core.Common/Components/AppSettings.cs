using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TCBase.Component;
using Microsoft.Extensions.Configuration;

namespace MongoDbHelper.Core.Common
{
    public class AppSettings : Singleton<AppSettings>
    {
        private IConfigurationRoot Config { get; }
        public AppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Kernel.Environment}.json", optional: true);
            Config = builder.Build();
        }
        public string GetConfig(string name)
        {
            return Instance.Config.GetSection(name).Value;
        }
    }
}

