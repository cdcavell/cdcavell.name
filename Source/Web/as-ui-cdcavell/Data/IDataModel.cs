﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace as_ui_cdcavell.Data
{
    /// <summary>
    /// Authorization UI DataModel Interface
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public interface IDataModel<T>
    {
        /// <value>int</value>
        int Id { get; set; }

        /// <value>bool</value>
        bool IsNew { get; }

        #region Instance Methods

        /// <summary>
        /// Add/Update method
        /// </summary>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>AddUpdate(CDCavellDbContext dbContext)</method>
        void AddUpdate(AuthorizationUiDbContext dbContext);

        /// <summary>
        /// Equate method
        /// </summary>
        /// <param name="obj">T</param>
        /// <returns>bool</returns>
        /// <method>Equals(T obj)</method>
        bool Equals(T obj);

        #endregion
    }
}