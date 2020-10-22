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
            try
            {
                Match value = Regex.Matches(text, @"\s+\d+(\.\d+)?")[0];
                text = value.ToString();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Match value = Regex.Matches(text, @"\d+(\.\d+)?")[0];
                text = value.ToString();
            }

            return Convert.ToDouble(text);
        }
    }
}
