using ModelLibrary;
using PathLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ExceptionsLogging
{
    public  class ExceptionLogger 
    {
        private readonly static string _filePath = MainPath.GetMainPath() + @"\ExceptionsLogging\Log.txt";
        private readonly static string _parsingFilePath = MainPath.GetMainPath() + @"\ExceptionsLogging\NotParsedElementsLog.txt";
        private static Object locker = new Object();

        //Logs Exception to a file
        public void Log (Exception ex)
        {
            lock(locker) { 
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }

                using (StreamWriter streamWriter = new StreamWriter(_filePath, true))
                {
                    streamWriter.WriteLine("Error:   " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        streamWriter.WriteLine("Error inner:   " + ex.InnerException.Message);
                    }
                    streamWriter.WriteLine("Occured At:    " + GetFullStackTrace(ex));
                    streamWriter.WriteLine("Time:    " + DateTime.Now);
                }
            }
        }


        public void LogProcessorParsingException(Processor processor, Exception ex)
        {
            lock (locker)
            {

                if (!File.Exists(_parsingFilePath))
                {
                    File.Create(_parsingFilePath).Close();
                }

                using (StreamWriter streamWriter = new StreamWriter(_parsingFilePath, true))
                {
                    streamWriter.WriteLine(ex.Message);
                    streamWriter.WriteLine("Processor ID is:   " + processor.Id);
                    streamWriter.WriteLine("Time:    " + DateTime.Now);
                }
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
