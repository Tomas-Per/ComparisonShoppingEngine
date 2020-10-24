﻿using ExceptionsLibrary;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            catch (ArgumentOutOfRangeException)
            {
                Match value = Regex.Matches(text, @"\d+(\.\d+)?")[0];
                text = value.ToString();
            }
            catch (Exception e)
            {
                throw new DataCustomException("Error happened while trying to parse: " + e.Message, this);
            }

            return Convert.ToDouble(text);
        }
    }
}