using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell DataModel base class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/12/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// | Christopher D. Cavell | 1.0.3.0 | 10/23/2020 | Initial build Authorization Service |~ 
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
        /// <method>AddUpdate(CDCavellDbContext dbContext)</method>
        public virtual void AddUpdate(CDCavellDbContext dbContext)
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
        /// <param name="dbContext"></param>
        public virtual void Delete(CDCavellDbContext dbContext)
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
