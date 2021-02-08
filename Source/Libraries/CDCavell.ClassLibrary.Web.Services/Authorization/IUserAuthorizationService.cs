using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Services.Authorization
{
    /// <summary>
    /// User Authorization Web Service Interface
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.1 | 02/07/2021 | User Authorization Web Service |~ 
    /// </revision>
    public interface IUserAuthorizationService
    {
        /// <summary>
        /// Initial Authorization
        /// </summary>
        /// <param name="ticketReceivedContext">TicketReceivedContext</param>
        /// <returns>Task&lt;UserAuthorizationModel&gt;</returns>
        Task<UserAuthorizationModel> InitialAuthorization(TicketReceivedContext ticketReceivedContext);
    }
}
