﻿using CDCavell.ClassLibrary.Web.Mvc.Models.Authorization;
using CDCavell.ClassLibrary.Web.Security;
using Microsoft.EntityFrameworkCore;
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
    /// | Christopher D. Cavell | 1.0.3.0 | 01/31/2021 | Initial build Authorization Service |~ 
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

        private string _token;
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Token
        {
            get { return AESGCM.Decrypt(_token); }
            set { _token = AESGCM.Encrypt(value); }
        }

        /// <value>string</value>
        [NotMapped]
        public string AccessToken
        {
            get { return JsonConvert.DeserializeObject<string>(Token); }
            set { Token = JsonConvert.SerializeObject(value); }
        }

        private string _object;
        /// <value>string</value>
        [Required]
        [DataType(DataType.Text)]
        public string Object 
        { 
            get { return AESGCM.Decrypt(_object); } 
            set { _object = AESGCM.Encrypt(value); }
        }

        /// <value>UserAuthorization</value>
        [NotMapped]
        public UserAuthorization UserAuthorization
        {
            get { return JsonConvert.DeserializeObject<UserAuthorization>(Object); }
            set { Object = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// Add/Update record
        /// </summary>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>AddUpdate(CDCavellDbContext dbContext)</method>
        public override void AddUpdate(CDCavellDbContext dbContext)
        {
            // Remove any stale Authorization records
            var query = dbContext.Authorization.Where(x => x.Created < DateTime.Now.AddDays(-1));
            if (query.Count() > 0)
                dbContext.Authorization.RemoveRange(query);

            // Update Id sequence in SQLite
            // For SQL Server use raw query DBCC CHECKIDENT ('table_name', RESEED, 1)
            long maxSequence = dbContext.Authorization.Count();
            if (maxSequence > 0)
                maxSequence = dbContext.Authorization.Max(x => x.Id);

            dbContext.Database.ExecuteSqlInterpolated($"UPDATE sqlite_sequence SET seq = {maxSequence} WHERE name = 'Authorization'");

            // Add/update record
            base.AddUpdate(dbContext);
        }

        #region Static Methods

        /// <summary>
        /// Get Authorization record
        /// </summary>
        /// <param name="claims">IEnumerable&lt;Claim&gt;</param>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <returns>Authorization</returns>
        /// <method>GetRecord(IEnumerable&lt;Claim&gt; claims, CDCavellDbContext dbContext)</method>
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
                    userAuthorization = authorization.UserAuthorization;
            }

            return userAuthorization;
        }

        #endregion
    }
}