using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace as_api_cdcavell.Data
{
    /// <class>IDataModel&lt;T&gt;</class>
    /// <summary>
    /// DataModel Interface
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/20/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public interface IDataModel<T>
    {
        /// <value>long</value>
        long Id { get; set; }

        /// <value>bool</value>
        bool IsNew { get; }

        #region Instance Methods

        /// <summary>
        /// Add/Update method
        /// </summary>
        /// <param name="dbContext">CDCavellDbContext</param>
        /// <method>AddUpdate(CDCavellDbContext dbContext)</method>
        void AddUpdate(AuthorizationServiceDbContext dbContext);

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
