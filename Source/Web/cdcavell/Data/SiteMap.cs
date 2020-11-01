using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell SiteMap Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.7 | 10/31/2020 | Integrate Bing’s Adaptive URL submission API with your website [#144](https://github.com/cdcavell/cdcavell.name/issues/144) |~ 
    /// </revision>
    public class SiteMap
    {
        /// <value>long</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <value>int</value>
        public int SiteMapId { get; set; }
        /// <value>string</value>
        public string Controller { get; set; }
        /// <value>string</value>
        public string Action { get; set; }
        /// <value>DateTime</value>
        public DateTime LastSubmitDate { get; set; }
    }
}
