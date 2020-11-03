using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell DataModel Interface
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/03/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
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
        void AddUpdate(CDCavellDbContext dbContext);

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
