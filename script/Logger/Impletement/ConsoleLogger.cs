using System;

namespace Logger
{
    public class ConsoleLogger : LoggerBase
    {
        public override void Write(string title, string content, InfoType type = InfoType.Debug)
        {
            switch (type)
            {
                case InfoType.Debug:
                    WriteTitleWithColor(title, content, ConsoleColor.Green);
                    break;
                case InfoType.Warning:
                    WriteTitleWithColor(title, content, ConsoleColor.Yellow);
                    break;
                case InfoType.Error:
                    WriteTitleWithColor(title, content, ConsoleColor.Red);
                    break;
            }
        }

        private void WriteTitleWithColor(string Title, string content, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor OriginalForeGround = Console.ForegroundColor;
            Console.ForegroundColor = foreground;
            Console.Write(Title);
            Console.ForegroundColor = OriginalForeGround;
            Console.Write(string.Format(" {0}", content));
        }
    }
}
