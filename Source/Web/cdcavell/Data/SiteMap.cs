using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        #region Static Methods

        /// <summary>
        /// Get count of controller and action in StieMap entity
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="dbContext"></param>
        /// <returns>int</returns>
        /// <method>GetCount(string controller, string action, CDCavellDbContext dbContext)</method>
        public static int GetCount(string controller, string action, CDCavellDbContext dbContext)
        {
            return dbContext.SiteMap
                .Where(x => x.Controller == controller.Clean())
                .Where(x => x.Action == action.Clean())
                .Count();
        }

        /// <summary>
        /// Get all StieMap entity records
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns>int</returns>
        /// <method>GetAllSiteMap(CDCavellDbContext dbContext)</method>
        public static List<SiteMap> GetAllSiteMap(CDCavellDbContext dbContext)
        {
            return dbContext.SiteMap.ToList();
        }

        #endregion
    }
}
