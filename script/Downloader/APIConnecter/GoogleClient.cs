using System;
using System.Net;

namespace GDataAPIConnecter
{
    public class GoogleClient : WebClient
    {
        private readonly CookieContainer _Container = null;

        public GoogleClient()
        {
            _Container = new CookieContainer();
        }

        public GoogleClient(CookieContainer container)
        {
            _Container = container;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest r = base.GetWebRequest(address);
            if (r is HttpWebRequest request)
            {
                request.CookieContainer = _Container;
            }
            return r;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = base.GetWebResponse(request, result);
            ReadCookies(response);
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        }

        private void ReadCookies(WebResponse r)
        {
            if (r is HttpWebResponse response)
            {
                CookieCollection cookies = response.Cookies;
                _Container.Add(cookies);
            }
        }
    }
}
