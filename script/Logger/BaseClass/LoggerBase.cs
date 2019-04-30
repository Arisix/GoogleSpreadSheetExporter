namespace Logger
{
    public abstract class LoggerBase : ILogger
    {
        public void Debug(string content)
        {
            WriteLine(content, InfoType.Debug);
        }

        public void Warning(string content)
        {
            WriteLine(content, InfoType.Warning);
        }

        public void Error(string content)
        {
            WriteLine(content, InfoType.Error);
        }

        public virtual void WriteLine(object infoData)
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

        public virtual void WriteLine(string title, string content, InfoType type = InfoType.Debug)
        {
            Write(title, string.Format("{0}\n", content), type);
        }

        public virtual void WriteLine(string content, InfoType type = InfoType.Debug)
        {
            Write(string.Format("{0}\n", content), type);
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
                    Write(content, type);
                }
                else
                {
                    Write(title, content, type);
                }
            }
            else
            {
                Error("Logger object must be InfoData.");
            }
        }

        public virtual void Write(string content, InfoType type = InfoType.Debug)
        {
            string title = string.Empty;

            int rightIndex = 0;
            if (content.StartsWith("[") && (rightIndex = content.IndexOf("]")) > 0)
            {
                title = content.Substring(0, rightIndex + 1);
                content = content.Remove(0, rightIndex + 1);
            }

            if (string.IsNullOrEmpty(title))
            {
                title = type.ToStaticString();
            }

            Write(title, content, type);
        }

        public abstract void Write(string title, string content, InfoType type = InfoType.Debug);
    }
}