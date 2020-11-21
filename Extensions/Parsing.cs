using System;
using System.Text.RegularExpressions;

namespace Parsing
{
    public static class Parsing
    {

        //replaces "," with "."
        //some data uses comma, some dot so this method allows to parse both variants of data
        private static string ReplaceSeperator(this string text)
        {
            string value = Regex.Replace(text, @",", ".");
            return value;
        }

        //deletes spaces from a string
        private static string DeleteSpaces (this string text)
        {
            string value = Regex.Replace(text, @"[\s+]", "");
            return value;
        }
        public static string DeleteMultipleSpaces(this string text)
        {
            
            string value = Regex.Replace(text, @"\s+", " ");
            return value;
        }
        public static string DeleteSpecialChars(this string text)
        {
            var texts = text.Split(' ');
            string value = "";
            foreach(var t in texts)
            {
                value += Regex.Replace(t, @"[^a-zA-Z0-9]", " ") + " ";
            }
            value = value.DeleteMultipleSpaces();
            return value;
        }
        //returns double value from a given string
        public static double ParseDouble(this string text)
        {
            text = text.ReplaceSeperator();
            text = text.DeleteSpaces();
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
        public static int ParseInt(this string text)
        {
            text = text.DeleteSpaces();

            try
            {
                Match value = Regex.Matches(text, @"\d+")[0];
                text = value.ToString();

            }
            catch (ArgumentOutOfRangeException)
            {
                return 0;
            }

            return Convert.ToInt32(text);
        }
    }
}
