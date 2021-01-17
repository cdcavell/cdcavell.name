using dis5_cdcavell.Models.Consent;

namespace dis5_cdcavell.Models.Device
{
    /// <summary>
    /// Device Authorization Input Model
    /// &lt;br /&gt;&lt;br /&gt;
    /// Copyright (c) Duende Software. All rights reserved.
    /// See https://duendesoftware.com/license/identityserver.pdf for license information. 
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.2.0 | 01/16/2021 | Initial build |~ 
    /// </revision>
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        /// <value>string</value>
        public string UserCode { get; set; }
    }
}
