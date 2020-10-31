namespace CDCavell.ClassLibrary.Web.Mvc.Models
{
    /// <summary>
    /// Page view model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    public class PageViewModel
    {
        /// <value>string</value>
        public string ReturnURL { get; set; }
        /// <value>string</value>
        public string RequestId { get; set; }
        /// <value>string</value>
        public string ErrorMessage { get; set; }

        /// <value>bool</value>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        /// <value>bool</value>
        public bool IsError => !string.IsNullOrEmpty(ErrorMessage);

        /// <value>string</value>
        public string Controller { get; set; }
        /// <value>string</value>
        public string Action { get; set; }
    }
}
