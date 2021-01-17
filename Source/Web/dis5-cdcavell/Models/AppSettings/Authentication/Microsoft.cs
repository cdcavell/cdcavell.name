namespace dis5_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Microsoft Authentication model
    /// &lt;br/&gt;&lt;br/&gt;
    /// https://go.microsoft.com/fwlink/?linkid=2083908
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// </revision>
    public class Microsoft
    {
        /// <value>string</value>
        public string ClientId { get; set; }
        /// <value>string</value>
        public string ClientSecret { get; set; }
        /// <value>string</value>
        public string TokenEndpoint { get; set; }
    }
}
