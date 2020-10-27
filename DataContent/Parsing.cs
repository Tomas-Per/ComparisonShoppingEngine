using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
//using ExceptionsLibrary;
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
                Match value = Regex.Matches(text, @"\d+(\.\d+)?")[0];
                text = value.ToString();
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e; //new DataCustomException("Error happened while parsing int", null);
            }

            return Convert.ToDouble(text);
        }
        public static int ParseInt(string text)
        {
            try
            {
                Match value = Regex.Matches(text, @"\d+")[0];
                text = value.ToString();
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e; //new DataCustomException("Error happened while parsing int", null);
            }

            return Convert.ToInt32(text);
        }
    }
}
