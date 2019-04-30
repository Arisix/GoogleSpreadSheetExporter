using System;

namespace Logger
{
    public class InfoData
    {
        public string title { get; private set; }
        public string content { get; private set; }
        public InfoType type { get; private set; }

        public InfoData(string content)
        {
            this.content = content;
        }

        public InfoData(string title, string content)
        {
            this.title = title;
            this.content = content;
        }

        public InfoData(string content, InfoType type)
        {
            this.content = content;
            this.type = type;
        }

        public InfoData(string title, string content, InfoType type)
        {
            this.title = title;
            this.content = content;
            this.type = type;
        }

        public void AddNewLine()
        {
            content += Environment.NewLine;
        }
    }
}
