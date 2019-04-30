using System;
using System.IO;
using System.Text;

namespace Logger
{
    public class LogFileLogger : LoggerBase
    {
        private StreamWriter _StreamWriter;
        private static readonly object _StreamWriterLock = new object();

        public LogFileLogger(string path)
        {
            try
            {
                File.Delete(path);
                _StreamWriter = new StreamWriter(path, false, Encoding.UTF8);
                _StreamWriter.AutoFlush = true;
            }
            catch(Exception e)
            {
                if(e is UnauthorizedAccessException ||
                    e is ArgumentException ||
                    e is ArgumentNullException ||
                    e is DirectoryNotFoundException ||
                    e is IOException ||
                    e is PathTooLongException)
                {
                    ConsoleColor OriginalForeGround = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[Error] ");
                    Console.ForegroundColor = OriginalForeGround;
                    Console.WriteLine("logfile path is wrong or it's using by another proccess.");
                }
            }
        }

        public override void Write(string title, string content, InfoType type = InfoType.Debug)
        {
            string output = string.Format("{0} {1}", title, content);
            _StreamWriter.Write(output);
        }
    }
}