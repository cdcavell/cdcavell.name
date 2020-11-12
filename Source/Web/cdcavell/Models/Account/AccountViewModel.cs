using cdcavell.Data;

namespace cdcavell.Models.Account
{
    /// <summary>
    /// Registration view model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.9 | 11/12/2020 | Implement Registration/Roles/Permissions [#183](https://github.com/cdcavell/cdcavell.name/issues/183) |~ 
    /// </revision>
    public class AccountViewModel
    {
        /// <value>Registration</value>
        public Registration Registration { get; set; }
    }
}
