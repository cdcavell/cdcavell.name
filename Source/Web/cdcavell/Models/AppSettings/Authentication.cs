using cdcavell.Classes;

namespace cdcavell.Models.AppSettings
{
    /// <summary>
    /// Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 10/12/2020 | Initial build |~ 
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// </revision>
    public class Authentication
    {
        /// <value>IdP</value>
        public IdP IdP { get; set; }
        /// <value>BingCustomSearch</value>
        public BingCustomSearch BingCustomSearch { get; set; }
        /// <value>BingWebmaster</value>
        public BingWebmasterModel BingWebmaster { get; set; }
    }
}
