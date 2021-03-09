namespace CDCavell.ClassLibrary.Web.Mvc.Models.AppSettings
{
    /// <summary>
    /// AuthorizationService model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/08/2021 | User Authorization Service |~ 
    /// </revision>
    public class AuthorizationService
    {
        /// <value>string</value>
        public string API { get; set; }
        /// <value>string</value>
        public string UI { get; set; }
        /// <value>string</value>
        public string Main { get; set; }


        /// <value>string</value>
        public string ApiTrim { get { return this.API.TrimEnd('/').TrimEnd('\\'); } }
        /// <value>string</value>
        public string UiTrim { get { return this.UI.TrimEnd('/').TrimEnd('\\'); } }
        /// <value>string</value>
        public string MainTrim { get { return this.Main.TrimEnd('/').TrimEnd('\\'); } }
    }
}
