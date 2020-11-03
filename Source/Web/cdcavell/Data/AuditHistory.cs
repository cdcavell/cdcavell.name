using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace cdcavell.Data
{
    /// <summary>
    /// CDCavell AuditHistory Entity
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/03/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    [Table("AuditHistory")]
    public class AuditHistory
    {
        /// <value>int</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <value>string</value>
        [DataType(DataType.Text)]
        public string ModifiedBy { get; set; }
        /// <value>DateTime?</value>
        [AllowNull]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; } = DateTime.MinValue;
        /// <value>string</value>
        [DataType(DataType.Text)]
        public string Application { get; set; }
        /// <value>string</value>
        [DataType(DataType.Text)]
        public string Entity { get; set; }
        /// <value>string</value>
        [DataType(DataType.Text)]
        public string State { get; set; }
        /// <value>string</value>
        [DataType(DataType.Text)]
        public string KeyValues { get; set; }
        /// <value>string</value>
        [DataType(DataType.Text)]
        public string OriginalValues { get; set; }
        /// <value>string</value>
        [DataType(DataType.Text)]
        public string CurrentValues { get; set; }
    }
}
