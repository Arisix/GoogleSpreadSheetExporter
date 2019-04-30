using Options;
using Patterns;
using System.Collections.Generic;

namespace Logger
{
    public class LoggerManager : Singleton<LoggerManager>, ILoggerManager
    {
        List<ILogger> LoggerPool = null;
        public override void Initialize()
        {
            LoggerPool = new List<ILogger>();

            LoggerBase fileLogger = new LogFileLogger(Config.LogFilePath);
            LoggerPool.Add(fileLogger);

            LoggerBase consoleLogger = new ConsoleLogger();
            LoggerPool.Add(consoleLogger);
        }

        public void Debug(string content)
        {
            WriteLine(content);
        }

        public void Error(string content)
        {
            WriteLine(content, InfoType.Error);
        }

        public void Warning(string content)
        {
            WriteLine(content, InfoType.Warning);
        }

        public void WriteLine(string content, InfoType type = InfoType.Debug)
        {
            InfoData data = new InfoData(content, type);
            WriteLine(data);
        }

        public void WriteLine(string title, string content, InfoType type = InfoType.Debug)
        {
            InfoData data = new InfoData(title, content, type);
            WriteLine(data);
        }

        public void WriteLine(object infoData)
        {
            if (infoData is InfoData)
            {
                InfoData data = infoData as InfoData;
                data.AddNewLine();

                Write(data);
            }
            else
            {
                Error("Logger object must be InfoData.");
            }
        }

        public void Write(string title, string content, InfoType type = InfoType.Debug)
        {
            InfoData data = new InfoData(title, content, type);
            Write(data);
        }

        public void Write(string content, InfoType type = InfoType.Debug)
        {
            InfoData data = new InfoData(content, type);
            Write(data);
        }

        public void Write(object infoData)
        {
            if (infoData is InfoData)
            {
                InfoData data = infoData as InfoData;
                string title = data.title;
                string content = data.content;
                InfoType type = data.type;

                if (string.IsNullOrEmpty(title))
                {
                    LoggerPool.ForEach(x => x.Write(content, type));
                }
                else
                {
                    LoggerPool.ForEach(x => x.Write(title, content, type));
                }
            }
            else
            {
                Error("Logger object must be InfoData.");
            }
        }
    }
}