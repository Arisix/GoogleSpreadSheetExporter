using Downloader;
using Options;
using System.Configuration;

class Program
{
    private static GoogleSpreadSheetDownloaderController _DownloadController = null;

    public static void Main(string[] args)
    {
        ParseArguments(args);
        Initailize();
        Download();
    }

    public static void ParseArguments(string[] args)
    {
        OptionsParser.Parse(args);
        if (Config.show_help)
        {
            OptionsParser.ShowHelp(Config.OptionSetting);
        }
    }

    public static void Initailize()
    {
        Config.CLIENT_ID = ConfigurationManager.AppSettings["CLIENT_ID"];
        Config.CLIENT_PRIVATE_KEY = ConfigurationManager.AppSettings["CLIENT_PRIVATE_KEY"];
        Config.LogFilePath = ConfigurationManager.AppSettings["LogFilePath"];
        Config.TokenPath = ConfigurationManager.AppSettings["TokenPath"];

        if(Config.IsSomethingEmpty)
        {
            System.Console.WriteLine("Something wrong happened. Please check your app.config and parameter is right.");
            OptionsParser.ShowHelp(Config.OptionSetting);
        }

        _DownloadController = GoogleSpreadSheetDownloaderController.Instance;
    }

    public static void Download()
    {
        if (_DownloadController.IsFirstTimeUser())
        {
            _DownloadController.GrantPermission();
        }
        _DownloadController.RefreshAccessToken();

        _DownloadController.SetupWebClient();

        _DownloadController.DownloadSpreadSheet();
    }
}