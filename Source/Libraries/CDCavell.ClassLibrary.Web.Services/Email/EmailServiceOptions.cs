using System.Net;

namespace CDCavell.ClassLibrary.Web.Services.Email
{
    /// <summary>
    /// Email Web Service Options
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/11/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class EmailServiceOptions
    {
        /// <value>string</value>
        public string Host { get; set; }
        /// <value>int</value>
        public int Port { get; set; }
        /// <value>NetworkCredential</value>
        public NetworkCredential Credentials { get; set; }
        /// <value>bool</value>
        public bool EnableSsl { get; set; }
    }
}
