using ExceptionsLibrary;
using System;
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
            catch (ArgumentOutOfRangeException)
            {
                throw; //new DataCustomException("Error happened while parsing int", null);
            }
            catch (Exception e)
            {
                throw new DataCustomException("Error happened while trying to parse: " + e.Message, ParseDouble(null));
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
            catch (ArgumentOutOfRangeException)
            {
                throw; //new DataCustomException("Error happened while parsing int", null);
            }

            return Convert.ToInt32(text);
        }
    }
}
