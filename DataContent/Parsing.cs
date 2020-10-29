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

        private static string DeleteSpaces (string text)
        {
            string value = Regex.Replace(text, @"[\s+]", "");
            return value;
        }

        public static double ParseDouble(string text)
        {
            text = ReplaceSeperator(text);
            text = DeleteSpaces(text);
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
            text = DeleteSpaces(text);

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
