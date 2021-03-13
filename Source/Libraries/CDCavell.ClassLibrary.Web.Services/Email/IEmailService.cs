using System.Net.Mail;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Services.Email
{
    /// <summary>
    /// Email Web Service Interface
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/13/2021 | User Authorization Web Service |~ 
    /// </revision>
    public interface IEmailService
    {
        /// <summary>
        /// Send mail message
        /// </summary>
        /// <param name="mailMessage">MailMessage</param>
        /// <returns>Task</returns>
        Task Send(MailMessage mailMessage);
    }
}
