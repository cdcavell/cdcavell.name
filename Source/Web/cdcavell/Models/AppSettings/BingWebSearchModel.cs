using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cdcavell.Models.AppSettings
{
    /// <summary>
    /// Bing Web Search Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.8 | 11/01/2020 | Bing Search APIs will transition from Azure Cognitive Services to Azure Marketplace on 31 October 2023 [#152](https://github.com/cdcavell/cdcavell.name/issues/152) |~ 
    /// </revision>
    public class BingWebSearchModel
    {
        /// <value>string</value>
        public string SubscriptionKey { get; set; }
        /// <value>string</value>
        public string CustomConfigId { get; set; }
    }
}
