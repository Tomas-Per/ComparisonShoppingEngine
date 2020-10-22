using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace DataContent
{
    public class Parsing
    {
        private static string ReplaceSeperator(string text)
        {
            string value = Regex.Replace(text, @",", ".");
            return value;
        }
        public static double ParseDouble(string text)
        {
            text = ReplaceSeperator(text);
            Match value = Regex.Matches(text, @"\s+\d+(\.\d+)?")[0];
            string val = value.ToString();
            return Convert.ToDouble(val);
        }
    }
}
