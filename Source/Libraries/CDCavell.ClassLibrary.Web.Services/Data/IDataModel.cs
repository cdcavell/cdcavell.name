namespace CDCavell.ClassLibrary.Web.Services.Data
{
    /// <summary>
    /// Authorization UI DataModel Interface
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/07/2021 | User Authorization Service |~ 
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
        /// <param name="dbContext">AuthorizationDbContext</param>
        /// <method>AddUpdate(AuthorizationDbContext dbContext)</method>
        void AddUpdate(AuthorizationDbContext dbContext);

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
