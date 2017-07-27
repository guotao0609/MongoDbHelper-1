using System;
using System.Collections.Generic;
using System.Text;
using TCBase.Component;

namespace MongoDbHelper.Core.Common.Components
{
    ///日志类封装, 满足天网日志收集规则. 
    public class SkyNetLogger
    {
        private class BaseLog : Log<BaseLog>
        {

        }
        public static void LogInfo(SkyNetMarker marker, string message, string filter1 = "", string filter2 = "")
        {
            string prefix = marker.ToString() + $"<{filter1}><{filter2}>";
            BaseLog.Info(prefix + message);
        }

        public static void LogError(SkyNetMarker marker, string message, Exception ex, string filter1 = "", string filter2 = "")
        {
            string prefix = marker.ToString() + $"<{filter1}><{filter2}>";
            BaseLog.Error(ex, prefix + message);
        }

        public static void LogError(SkyNetMarker marker, string message, string filter1 = "", string filter2 = "")
        {
            string prefix = marker.ToString() + $"<{filter1}><{filter2}>";
            BaseLog.Error(prefix + message);
        }

        public static void LogWarn(SkyNetMarker marker, string message, string filter1 = "", string filter2 = "")
        {
            string prefix = marker.ToString() + $"<{filter1}><{filter2}>";
            BaseLog.Warn(prefix + message);
        }
        public static void LogWarn(SkyNetMarker marker, string message, Exception ex, string filter1 = "", string filter2 = "")
        {
            string prefix = marker.ToString() + $"<{filter1}><{filter2}>";
            BaseLog.Warn(ex, prefix + message);
        }
    }

    public class SkyNetMarker
    {
        public string Module { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Subcategory { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"<{Module}><{Category}><{Subcategory}>";
        }

        public SkyNetMarker(string module = "", string cateory = "", string subcategory = "")
        {
            if (!string.IsNullOrWhiteSpace(module))
            {
                this.Module = module;
            }
            if (!string.IsNullOrWhiteSpace(cateory))
            {
                this.Category = cateory;
            }
            if (!string.IsNullOrWhiteSpace(subcategory))
            {
                this.Subcategory = subcategory;
            }
        }
    }

}
