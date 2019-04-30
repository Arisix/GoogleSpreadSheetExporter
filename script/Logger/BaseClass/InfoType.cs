namespace Logger
{
    public enum InfoType
    {
        Debug = 0,
        Warning,
        Error
    }

    public static class InfoTypeExtensions
    {
        public static string ToStaticString(this InfoType type)
        {
            string result = string.Empty;
            switch (type)
            {
                case InfoType.Debug:
                    result = "[Debug]";
                    break;
                case InfoType.Warning:
                    result = "[Warning]";
                    break;
                case InfoType.Error:
                    result = "[Error]";
                    break;
            }

            return result;
        }
    }
}
