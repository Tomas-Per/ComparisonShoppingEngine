using PathLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ExceptionsLogging
{
    public static class ExceptionLogger 
    {
        private readonly static string _filePath = MainPath.GetMainPath() + @"\ExceptionsLogging\Log.txt";

        //Logs Exception to a file
        public static void Log (Exception ex)
        {
            using (StreamWriter streamWriter = new StreamWriter(_filePath, true))
            {
                streamWriter.WriteLine("Error:   " + ex.Message);
                streamWriter.WriteLine("Occured At:    " + GetFullStackTrace(ex));
                streamWriter.WriteLine("Time:    " + DateTime.Now);
                streamWriter.Close();
            }
        }


        //returns Exception's full stack trace as string
        private static string GetFullStackTrace(Exception x)
        {
            var st = new StackTrace(x, true);
            var frames = st.GetFrames();
            var traceString = new StringBuilder();

            foreach (var frame in frames)
            {
                if (frame.GetFileLineNumber() < 1)
                    continue;

                traceString.Append("File: " + frame.GetFileName());
                traceString.Append(", Method:" + frame.GetMethod().Name);
                traceString.Append(", LineNumber: " + frame.GetFileLineNumber());
                traceString.Append("  -->  ");
            }

            return traceString.ToString();
        }

    }
}
