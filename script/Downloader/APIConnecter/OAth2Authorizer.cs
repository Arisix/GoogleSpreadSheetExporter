using Google.GData.Client;
using Options;
using Patterns;
using System;
using System.IO;
using System.Text;

namespace GDataAPIConnecter
{
    class OAth2Authorizer : Singleton<OAth2Authorizer>
    {
        private OAuth2Parameters _parameters;

        private readonly string CLIENT_ID = Config.CLIENT_ID;
        private readonly string CLIENT_PRIVATE_KEY = Config.CLIENT_PRIVATE_KEY;
        private readonly string SCOPE = "https://www.googleapis.com/auth/drive.metadata.readonly https://www.googleapis.com/auth/drive.readonly";
        private readonly string REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

        public override void Initialize()
        {
            _parameters = new OAuth2Parameters
            {
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_PRIVATE_KEY,
                RedirectUri = REDIRECT_URI,

                Scope = SCOPE,
                AccessCode = "authorization_code"
            };
        }

        public string RequestAccessCodeURL()
        {
            return OAuthUtil.CreateOAuth2AuthorizationUrl(_parameters);
        }

        public string SetAccessCodeAndRequestToken(string accessCode, Action<bool> Success = null)
        {
            _parameters.AccessCode = accessCode;
            _parameters.AccessToken = null;
            return GetAccessToken(Success);
        }

        public string GetAccessToken(Action<bool> Success = null)
        {
            if (_parameters.AccessToken == null)
            {
                var RefreshToken = GetRefreshToken();
                if (string.IsNullOrEmpty(RefreshToken))
                {
                    if (string.IsNullOrEmpty(_parameters.AccessCode))
                    {
                        Success?.Invoke(false);
                    }
                    OAuthUtil.GetAccessToken(_parameters);
                    SetRefreshToken(_parameters.RefreshToken);
                }
                else
                {
                    _parameters.RefreshToken = GetRefreshToken();
                    OAuthUtil.RefreshAccessToken(_parameters);
                }
            }

            Success?.Invoke(true);
            return _parameters.AccessToken;
        }
        
        public GOAuth2RequestFactory GetRequestFactory()
        {
            GOAuth2RequestFactory requestFactory = new GOAuth2RequestFactory(null, "Jenkins", _parameters);
            return requestFactory;
        }

        public DateTime GetAccessTokenExpiryDate { get { return _parameters.TokenExpiry; } }

        public string GetRefreshToken()
        {
            return File.Exists(Config.TokenPath) ? File.ReadAllText(Config.TokenPath) : string.Empty;
        }

        private void SetRefreshToken(string refreshToken)
        {
            File.WriteAllText(Config.TokenPath, refreshToken, Encoding.UTF8);
        }
    }
}
