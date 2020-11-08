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
    /// | Christopher D. Cavell | 1.0.0.8 | 11/01/2020 | Bing Search APIs will transition from Azure Cognitive Services to Azure Marketplace on 31 October 2023 [#152](https://github.com/cdcavell/cdcavell.name/issues/152) |~ 
    /// | Christopher D. Cavell | 1.0.0.9 | 11/08/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class Authentication
    {
        /// <value>IdP</value>
        public IdP IdP { get; set; }
        /// <value>BingCustomSearch</value>
        public BingCustomSearch BingCustomSearch { get; set; }
        /// <value>BingWebSearch</value>
        public BingWebSearchModel BingWebSearch { get; set; }
        /// <value>BingWebmaster</value>
        public BingWebmasterModel BingWebmaster { get; set; }
        /// <value>reCAPTCHA</value>
        public reCAPTCHA reCAPTCHA { get; set; }
    }
}
