namespace Winika.Lib.HttpCommunication
{
    using System;
    using System.IO;
    using System.Net;

    /// <summary>
    /// Manager class for HTTP communication.
    /// </summary>
    public class HttpRequestManager
    {
        /// <summary>
        /// User agent to be used in the requests.
        /// </summary>
        private readonly string _userAgent;

        /// <summary>
        /// Cookies to send / receive in the request.
        /// </summary>
        private Cookie[] _cookies;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestManager"/> class.
        /// </summary>
        public HttpRequestManager()
        {
            var version = VersionTools.VersionReader.GetVersion();
            this._userAgent = string.Format("nxu-winika-v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            this._cookies = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestManager"/> class.
        /// </summary>
        /// <param name="cookies">Cookies to be used in the requests.</param>
        public HttpRequestManager(Cookie[] cookies) : this()
        {
            this._cookies = cookies;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestManager"/> class.
        /// </summary>
        /// <param name="cookies">Cookies to be used in the requests.</param>
        /// <param name="userAgent">User agent to be used in the requests.</param>
        public HttpRequestManager(Cookie[] cookies, string userAgent)
        {
            this._cookies = cookies;
            this._userAgent = userAgent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestManager"/> class.
        /// </summary>
        /// <param name="userAgent">User agent to be used in the requests.</param>
        public HttpRequestManager(string userAgent)
        {
            this._cookies = null;
            this._userAgent = userAgent;
        }

        /// <summary>
        /// Executes a GET request.
        /// </summary>
        /// <param name="uri">Request URI.</param>
        /// <returns>Response body.</returns>
        public string Get(string uri)
        {
            var req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = WebRequestMethods.Http.Get;
            req.CookieContainer = this.GetCookieContainer();
            req.UserAgent = this._userAgent;

            try
            {
                using (var resp = (HttpWebResponse)req.GetResponse())
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    using (var reader = new StreamReader(resp.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                throw new Exception("Network communication error.");
            }

        }

        /// <summary>
        /// Converts the cookie array to a cookie container.
        /// </summary>
        /// <returns>Cookies in a cookie container.</returns>
        private CookieContainer GetCookieContainer()
        {
            var container = new CookieContainer();

            if (this._cookies == null)
            {
                return container;
            }

            foreach (var cookie in this._cookies)
            {
                container.Add(cookie);
            }

            return container;
        }
    }
}
