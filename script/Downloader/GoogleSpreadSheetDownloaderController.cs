using System;
using Patterns;
using Logger;
using Item;
using System.Net;
using System.Text;

namespace Downloader
{
    public class GoogleSpreadSheetDownloaderController : Singleton<GoogleSpreadSheetDownloaderController>
    {
        private GoogleSpreadsheetDownloader _Downloader = null;
        private ExportItemManager _ItemManager = null;
        private LoggerManager _Logger = null;

        string _AccessCode = string.Empty;

        public override void Initialize()
        {
            _Downloader = GoogleSpreadsheetDownloader.Instance;
            _Logger = LoggerManager.Instance;
            _ItemManager = ExportItemManager.Instance;
        }

        public bool IsFirstTimeUser()
        {
            return string.IsNullOrEmpty(_Downloader.GetRefreshToken());
        }

        public void GrantPermission()
        {
            _Logger.WriteLine("[GrantPermission] Grant permission for this app to access Google Sheet API on your behalf.");
            string AccessCodeURL = _Downloader.RequestAccessCodeURL();
            _Logger.WriteLine(string.Format("[GrantPermission] Visit this Url and paste your access code:{0}", AccessCodeURL));
            _Downloader.OpenUrlWithDefaultBrowser(AccessCodeURL);
            _Logger.Write("[GrantPermission] Access Code: ");
            _AccessCode = Console.ReadLine();
            _Logger.WriteLine("[GrantPermission] Requesting Access Token from Google. Please wait...");
        }

        public void RefreshAccessToken()
        {
            _Downloader.SetAccessCodeAndRequestToken(_AccessCode, (Success) =>
            {
                if (Success == false)
                {
                    string message = "Fail in requesting new Refresh Token and Access Token. Access Code cannot be empty.";
                    _Logger.Error(message);
                }
            });
            _Logger.WriteLine(string.Format("[TokenInfo] Access Token: {0}", _Downloader.GetAccessToken()));
            var ExpiryDate = _Downloader.GetAccessTokenExpiryDate;
            _Logger.WriteLine(string.Format("[TokenInfo] Expiry At: {0} {1}", ExpiryDate.ToShortDateString(), ExpiryDate.ToShortTimeString()));
            _Logger.WriteLine(string.Format("[TokenInfo] Refresh Token: {0}", _Downloader.GetRefreshToken()));
        }

        public void SetupWebClient()
        {
            _Downloader.SetupWebClient();
        }

        public void DownloadSpreadSheet()
        {
            _Logger.WriteLine("[Download] Begin to download spread sheet.");
            var list = _ItemManager.GetAllItems();
            foreach (var item in list)
            {
                _Logger.WriteLine(string.Format("[Download] Processing \"{0}\"...", item.Name));

                string output = _Downloader.DownloadSpreadSheet(item.Key, item.Gid);
                _ItemManager.Write(item.FilePath, output);

                _Logger.WriteLine(string.Format("[Complete] Success to export \"{0}\".", item.FilePath));
            }

            _ItemManager.Dispose();
            _Logger.WriteLine("[Complete] All Table is Complete.");
        }
    }
}