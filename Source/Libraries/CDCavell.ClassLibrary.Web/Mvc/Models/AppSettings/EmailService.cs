namespace CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings
{
    /// <summary>
    /// Site Administrator model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/13/2021 | User Authorization Service |~ 
    /// </revision>
    public class EmailService
    {
        /// <value>string</value>
        public string Host { get; set; }
        /// <value>int</value>
        public int Port { get; set; }
        /// <value>bool</value>
        public bool EnableSsl { get; set; }
        /// <value>string</value>
        public string UserId { get; set; }
        /// <value>string</value>
        public string Password { get; set; }
        /// <value>string</value>
        public string Email { get; set; }
    }
}
