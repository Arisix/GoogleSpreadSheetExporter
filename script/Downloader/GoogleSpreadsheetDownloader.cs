using GDataAPIConnecter;
using Options;
using Patterns;
using System;
using System.Text;

namespace Downloader
{
    public class GoogleSpreadsheetDownloader : Singleton<GoogleSpreadsheetDownloader>
    {
        #region ===== Object Handler =====
        private OAth2Authorizer _OAth2Authorizer = null;
        private GoogleClient _WebClient = null;
        #endregion

        #region ===== Initialization =====
        public override void Initialize()
        {
            _OAth2Authorizer = OAth2Authorizer.Instance;
            _WebClient = new GoogleClient();
        }
        #endregion

        #region ===== Connecter =====
        public string RequestAccessCodeURL()
        {
            return _OAth2Authorizer.RequestAccessCodeURL();
        }

        public string GetAccessToken()
        {
            return _OAth2Authorizer.GetAccessToken();
        }

        public void SetAccessCodeAndRequestToken(string AccessCode, Action<bool> Success = null)
        {
            _OAth2Authorizer.SetAccessCodeAndRequestToken(AccessCode, Success);
        }

        public string GetRefreshToken()
        {
            return _OAth2Authorizer.GetRefreshToken();
        }

        public DateTime GetAccessTokenExpiryDate { get { return _OAth2Authorizer.GetAccessTokenExpiryDate; } }

        public void OpenUrlWithDefaultBrowser(string url)
        {
            UrlAccesser.OpenUrlWithDefaultBrowser(url);
        }
        #endregion

        #region ===== Web Client =====
        public void SetupWebClient()
        {
            _WebClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
            _WebClient.Headers.Add("DNT", "1");
            _WebClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            _WebClient.Headers.Add("Accept-Encoding", "deflate");
            _WebClient.Headers.Add("Accept-Language", "zh-tw,zh;q=0.5");
            _WebClient.Headers.Add("Authorization", "Bearer " + GetAccessToken());
            _WebClient.Encoding = Encoding.UTF8;
        }

        public string DownloadSpreadSheet(string key, string uid)
        {
            string url = string.Format(Config.SpreadSheetUrlPattern, key, uid);
            return _WebClient.DownloadString(url);
        }
        #endregion
    }
}
