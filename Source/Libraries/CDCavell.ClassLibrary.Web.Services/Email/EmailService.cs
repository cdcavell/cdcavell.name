using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CDCavell.ClassLibrary.Web.Services.Email
{
    /// <summary>
    /// Email Web Service
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.3 | 03/11/2021 | User Authorization Web Service |~ 
    /// </revision>
    public class EmailService : IEmailService
    {
        private readonly Logger _logger;
        private readonly string _host;
        private readonly int _port;
        private readonly NetworkCredential _credentials;
        private readonly bool _enableSsl;

        private MailMessage _mailMessage;

        /// <value>MailMessage</value>
        public MailMessage MailMessage { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">ILogger&lt;EmailService&gt;</param>
        /// <param name="options">IOptions&lt;EmailServiceOptions&gt;</param>
        /// <method>EmailService(ILogger&lt;EmailService&gt; logger, IOptions&lt;EmailServiceOptions&gt; options)</method>
        public EmailService(ILogger<EmailService> logger, IOptions<EmailServiceOptions> options)
        {
            _logger = new Logger(logger);
            _host = options.Value.Host;
            _port = options.Value.Port;
            _credentials = options.Value.Credentials;
            _enableSsl = options.Value.EnableSsl;

            _mailMessage = new MailMessage();
        }

        /// <summary>
        /// Send mail message
        /// </summary>
        /// <returns>Task</returns>
        /// <exception cref="Exception">Invalid Property</exception>
        public Task Send()
        {
            if (_mailMessage.To.Count == 0)
                throw new Exception("Invalid Property", new Exception("MailMessage.To required"));

            if (string.IsNullOrEmpty(_mailMessage.From.Address))
                throw new Exception("Invalid Property", new Exception("MailMessage.From.Address required"));

            if (string.IsNullOrEmpty(_mailMessage.Subject))
                throw new Exception("Invalid Property", new Exception("MailMessage.Subject required"));

            if (string.IsNullOrEmpty(_mailMessage.Body))
                throw new Exception("Invalid options", new Exception("MailMessage.Body required"));

            SmtpClient smcl = new SmtpClient();
            smcl.Host = _host;
            smcl.Port = _port;
            smcl.Credentials = _credentials;
            smcl.EnableSsl = _enableSsl;

            return smcl.SendMailAsync(_mailMessage);
        }
    }
}
