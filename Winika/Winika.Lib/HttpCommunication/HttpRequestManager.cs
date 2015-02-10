namespace Winika.Lib.HttpCommunication
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;

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
        private readonly CookieContainer _cookies;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestManager"/> class.
        /// </summary>
        public HttpRequestManager()
        {
            var version = VersionTools.VersionReader.GetVersion();
            this._userAgent = string.Format("nxu-winika-v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            this._cookies = new CookieContainer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestManager"/> class.
        /// </summary>
        /// <param name="userAgent">User agent to be used in the requests.</param>
        public HttpRequestManager(string userAgent)
        {
            this._cookies = new CookieContainer();
            this._userAgent = userAgent;
        }

        /// <summary>
        /// Executes a GET request.
        /// </summary>
        /// <param name="uri">Request URI.</param>
        /// <returns>Response body.</returns>
        public async Task<string> Get(string uri)
        {
            // Set cookies
            var handler = new HttpClientHandler();
            handler.CookieContainer = this._cookies;

            // Create HTTP client
            var client = new HttpClient(handler);

            // Set user agent
            client.DefaultRequestHeaders.Add("User-Agent", this._userAgent);

            // Send request
            return await client.GetStringAsync(uri);
        }

        /// <summary>
        /// Executes a POST request.
        /// </summary>
        /// <param name="uri">Request URI.</param>
        /// <param name="requestBody">Request body.</param>
        /// <returns>Response body.</returns>
        public async Task<string> Post(string uri, IEnumerable<KeyValuePair<string, string>> requestBody)
        {
            // Set cookies
            var handler = new HttpClientHandler();
            handler.CookieContainer = this._cookies;

            // Create HTTP client
            var client = new HttpClient(handler);

            // Set user agent
            client.DefaultRequestHeaders.Add("User-Agent", this._userAgent);

            // Set POST data
            var content = new FormUrlEncodedContent(requestBody);

            // Send request
            var response = await client.PostAsync(uri, content);

            // Get response
            return await response.Content.ReadAsStringAsync();
        }
    }
}
