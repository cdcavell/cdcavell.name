using CDCavell.ClassLibrary.Commons.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

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
        private List<MailAddress> _toAddresses;
        private List<MailAddress> _fromAddresses;
        private string _body;
        private bool _isBodyHtml;
        private string _subject;

        /// <value>MailMessage</value>
        public MailMessage MailMessage { get; set; }
        /// <value>List&lt;MailAddress&gt;</value>
        public List<MailAddress> ToAddresses { get; set; }
        /// <value>List&lt;MailAddress&gt;</value>
        public List<MailAddress> FromAddresses { get; set; }
        /// <value>string</value>
        public string Body { get; set; }
        /// <value>bool</value>
        public bool IsBodyHtml { get; set; }
        /// <value>string</value>
        public string Subject { get; set; }

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
        }
    }
}
