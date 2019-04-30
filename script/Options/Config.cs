namespace Options
{
    public sealed class Config
    {
        public static bool show_help = false;

        public static string OutputPath;
        public static string ConfigPath;
        public static string LogFilePath;
        public static string TokenPath;
        public static string CLIENT_ID;
        public static string CLIENT_PRIVATE_KEY;

        public readonly static OptionSet OptionSetting = new OptionSet() {
            { "i|input=", "Config file path.", v => ConfigPath = v },
            { "o|output=", "Output directory path.", v => OutputPath = v },
            { "h|help",  "show this message and exit", v => show_help = v != null },
        };

        public static bool IsSomethingEmpty
        {
            get
            {
                return string.IsNullOrEmpty(OutputPath) ||
                    string.IsNullOrEmpty(ConfigPath) ||
                    string.IsNullOrEmpty(LogFilePath) ||
                    string.IsNullOrEmpty(TokenPath) ||
                    string.IsNullOrEmpty(CLIENT_ID) ||
                    string.IsNullOrEmpty(CLIENT_PRIVATE_KEY);
            }
        }

        public readonly static string SpreadSheetUrlPattern = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";
    }
}