using ExceptionsLibrary;
using System;
using System.Text.RegularExpressions;

namespace DataContent
{
    public class Parsing
    {

        //replaces "," with "."
        //some data uses comma, some dot so this method allows to parse both variants of data
        private static string ReplaceSeperator(string text)
        {
            string value = Regex.Replace(text, @",", ".");
            return value;
        }

        //deletes spaces from a string
        private static string DeleteSpaces (string text)
        {
            string value = Regex.Replace(text, @"[\s+]", "");
            return value;
        }

        //returns double value from a given string
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
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("Error happened while trying to parse: " + e.Message);
            }

            return Convert.ToDouble(text);
        }

        //returns int value from a given string
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
                throw;
            }

            return Convert.ToInt32(text);
        }
    }
}
