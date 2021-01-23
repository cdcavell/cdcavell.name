using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using CDCavell.ClassLibrary.Web.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell SiteMap Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 10/23/2020 | Initial build Authorization Service |~ 
    /// </revision>
    [Table("Authorization")]
    public class Authorization : DataModel<Authorization>
    {
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Guid { get; set; }
        /// <value>DateTime</value>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        private string _object;
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Object 
        { 
            get { return AESGCM.Decrypt(_object); } 
            set { _object = AESGCM.Encrypt(value); }
        }

        /// <summary>
        /// Add/Update record
        /// </summary>
        /// <method>AddUpdate(CDCavellDbContext dbContext)</method>
        public override void AddUpdate(CDCavellDbContext dbContext)
        {
            // Remove any stale Authorization records
            var query = dbContext.Authorization.Where(x => x.Created < DateTime.Now.AddDays(-1));
            if (query.Count() > 0)
                dbContext.Authorization.RemoveRange(query);

            base.AddUpdate(dbContext);
        }

        #region Static Methods

        /// <summary>
        /// Get Authorization record
        /// </summary>
        /// <param name="claims">IEnumerable&lt;Claim&gt;</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <returns>Authorization</returns>
        /// <method>Get(IEnumerable&lt;Claim&gt; claims, CDCavellDbContext dbContext)</method>
        public static Authorization GetRecord(IEnumerable<Claim> claims, CDCavellDbContext dbContext)
        {
            Authorization authorization = null;
            Claim authorizationClaim = claims.Where(x => x.Type == "authorization").FirstOrDefault();
            if (authorizationClaim != null)
                authorization = dbContext.Authorization
                    .Where(x => x.Guid == authorizationClaim.Value.ToString())
                    .FirstOrDefault();

            if (authorization == null)
                authorization = new Authorization();

            return authorization;
        }

        /// <summary>
        /// Get UserAuthorization object from data record
        /// </summary>
        /// <param name="claims">IEnumerable&lt;Claim&gt;</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <returns>UserAuthorization</returns>
        /// <method>UserAuthorization GetUser(IEnumerable&lt;Claim&gt; claims, CDCavellDbContext dbContext)</method>
        public static UserAuthorization GetUser(IEnumerable<Claim> claims, CDCavellDbContext dbContext)
        {
            UserAuthorization userAuthorization = new UserAuthorization();
            Claim authorizationClaim = claims.Where(x => x.Type == "authorization").FirstOrDefault();
            if (authorizationClaim != null)
            {
                Data.Authorization authorization = dbContext.Authorization
                    .Where(x => x.Guid == authorizationClaim.Value.ToString())
                    .FirstOrDefault();

                if (authorization != null)
                    userAuthorization = JsonConvert.DeserializeObject<UserAuthorization>(authorization.Object);
            }

            return userAuthorization;
        }

        #endregion
    }
}
