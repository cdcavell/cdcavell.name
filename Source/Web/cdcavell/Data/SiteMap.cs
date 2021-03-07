using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
    /// | Christopher D. Cavell | 1.0.0.9 | 11/03/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    [Table("SiteMap")]
    public class SiteMap : DataModel<SiteMap>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Controller { get; set; }
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Action { get; set; }
        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        public DateTime? LastSubmitDate { get; set; } = DateTime.MinValue;

        #region Instance Methods

        #endregion

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

        /// <summary>
        /// Get StieMap entity records that have not been submitted
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns>int</returns>
        /// <method>GetNotSubmittedSiteMap(CDCavellDbContext dbContext)</method>
        public static List<SiteMap> GetNotSubmittedSiteMap(CDCavellDbContext dbContext)
        {
            return dbContext.SiteMap
                .Where(x => x.LastSubmitDate == DateTime.MinValue)
                .ToList();
        }

        /// <summary>
        /// Get StieMap entity records
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="dbContext"></param>
        /// <returns>int</returns>
        /// <method>GetSiteMap(string controller, string action, CDCavellDbContext dbContext)</method>
        public static SiteMap GetSiteMap(string controller, string action, CDCavellDbContext dbContext)
        {
            return dbContext.SiteMap
                .Where(x => x.Controller == controller.Clean())
                .Where(x => x.Action == action.Clean())
                .FirstOrDefault();
        }

        #endregion
    }
}
