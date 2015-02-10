

namespace Winika.Lib.Profile
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Exceptions;
    using HttpCommunication;

    public class PlayerProfile
    {
        /// <summary>
        /// Hides default constructor. Instances should be created with PlayerProfile.Login().
        /// </summary>
        private PlayerProfile() { }

        public HttpRequestManager RequestManager { get; private set; }
        public string FullUri { get; private set; }

        public string UserName { get; set; }
        public int GoldAmount { get; set; }
        public int MaxShips { get; set; }
        public int AvailableShips { get; set; }

        /// <summary>
        /// Tries to log in a player and returns the profile of the player if successful.
        /// </summary>
        /// <param name="server">The selected server.</param>
        /// <param name="serverBaseUri">Base URI of the server.</param>
        /// <param name="userAgent">User Agent to use.</param>
        /// <param name="userName">Name of the player.</param>
        /// <param name="password">Password of the player.</param>
        /// <returns>Profile of the player.</returns>
        public async static Task<PlayerProfile> Login(  string server, 
                                                        string serverBaseUri, 
                                                        string userAgent, 
                                                        string userName, 
                                                        string password)
        {
            var reqMan = new HttpRequestManager(userAgent);

            // Create login data
            var data = new List<KeyValuePair<string, string>>();
            data.Add(new KeyValuePair<string, string>("uni_url", server + "." + serverBaseUri));
            data.Add(new KeyValuePair<string, string>("name", userName));
            data.Add(new KeyValuePair<string, string>("password", password));

            // Send login request
            var fullUri = string.Format("http://{0}.{1}", server, serverBaseUri);
            string response;
            try
            {
                response = await reqMan.Post(fullUri + "/index.php?action=loginAvatar&function=login", data);
            }
            catch
            {
                throw new InvalidLoginException("Error during the login process, the server cannot be reached.");    
            }

            // Check if login successful
            if (!response.Contains("logout"))
            {
                // Unsuccessful
                throw new InvalidLoginException("Invalid username or password");
            }

            // Login successful, create profile
            var profile = new PlayerProfile {FullUri = fullUri, RequestManager = reqMan};

            return profile;
        }
    }
}
