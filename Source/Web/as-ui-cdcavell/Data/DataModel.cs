using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace as_ui_cdcavell.Data
{
    /// <summary>
    /// Authorization UI DataModel base class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/30/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public abstract partial class DataModel<T> : IDataModel<DataModel<T>> where T : DataModel<T>
    {
        /// <value>int</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <value>bool</value>
        [NotMapped]
        public bool IsNew
        {
            get
            {
                if (this.Id == 0)
                    return true;

                return false;
            }
        }

        #region Instance Methods

        /// <summary>
        /// Add/Update record
        /// </summary>
        /// <method>AddUpdate(AuthorizationUiDbContext dbContext)</method>
        public virtual void AddUpdate(AuthorizationUiDbContext dbContext)
        {
            if (this.IsNew)
                dbContext.Add<DataModel<T>>(this);
            else
                dbContext.Update<DataModel<T>>(this);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="dbContext">AuthorizationUiDbContext</param>
        public virtual void Delete(AuthorizationUiDbContext dbContext)
        {
            if (!this.IsNew)
            {
                dbContext.Attach<DataModel<T>>(this);
                dbContext.Remove<DataModel<T>>(this);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Equate method
        /// </summary>
        /// <param name="obj">T</param>
        /// <returns>bool</returns>
        /// <method>Equals(DataModel&lt;T&gt; obj)</method>
        public bool Equals(DataModel<T> obj)
        {
            return (this == obj);
        }

        #endregion
    }
}
