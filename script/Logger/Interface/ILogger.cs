namespace Logger
{
    interface ILogger
    {
        void Debug(string content);
        void Warning(string content);
        void Error(string content);
        void WriteLine(string title, string content, InfoType type = InfoType.Debug);
        void WriteLine(string content, InfoType type = InfoType.Debug);
        void WriteLine(object infoData);
        void Write(string title, string content, InfoType type = InfoType.Debug);
        void Write(string content, InfoType type = InfoType.Debug);
        void Write(object infoData);
    }
}
