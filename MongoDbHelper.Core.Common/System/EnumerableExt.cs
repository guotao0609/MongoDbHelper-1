using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExt
    {

        public static string ToArrayString(this IEnumerable<int> source, char spliter)
        {
            var result = string.Empty;
            foreach (var item in source)
            {
                result += item.ToString() + spliter;
            }
            result = result.TrimEnd(spliter);
            return result;
        }

        public static string ToArrayString(this IEnumerable<long> source, char spliter)
        {
            var result = string.Empty;
            foreach (var item in source)
            {
                result += item.ToString() + spliter;
            }
            result = result.TrimEnd(spliter);
            return result;
        }

        public static string ToArrayString(this IEnumerable<string> source, char spliter, string leftchar = "", string rightchar = "")
        {
            var result = string.Empty;
            foreach (var item in source)
            {
                result += leftchar + item.ToString() + rightchar + spliter;
            }
            result = result.TrimEnd(spliter);
            return result;
        }

    }
}
