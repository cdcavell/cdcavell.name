using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cdcavell.Services.Authorization
{
    /// <summary>
    /// Application Authorization Web Service Interface
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/09/2021 | User Authorization Web Service |~ 
    /// </revision>
    public interface IApplicationAuthorizationService
    {
        /// <summary>
        /// Initial Authorization
        /// </summary>
        /// <param name="ticketReceivedContext">TicketReceivedContext</param>
        /// <returns>Task&lt;List&lt;Claim&gt;&gt;</returns>
        Task<List<Claim>> InitialAuthorization(TicketReceivedContext ticketReceivedContext);
    }
}
