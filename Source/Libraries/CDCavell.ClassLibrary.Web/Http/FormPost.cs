using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;

namespace CDCavell.ClassLibrary.Web.Http
{
    /// <summary>
    /// Class to post form data to given Respose object
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.3.0 | 01/31/2021 | Initial build Authorization Service |~ 
    /// </revision>
    public class FormPost
    {
        private HttpResponse _response;
        private List<KeyValuePair<string, string>> _items;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="response">HttpResponse</param>
        /// <method>FormPost(HttpResponse response)</method>
        public FormPost(HttpResponse response)
        {
            _response = response;
            _items = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Add form items
        /// </summary>
        /// <param name="items">List&lt;KeyValuePair&lt;string, string&gt;&gt;</param>
        /// <method>Add(List&lt;KeyValuePair&lt;string, string&gt;&gt; items)</method>
        public void Add(List<KeyValuePair<string, string>> items)
        {
            _items.AddRange(items);
        }

        /// <summary>
        /// Add form item
        /// </summary>
        /// <param name="item">KeyValuePair&lt;string, string&gt;</param>
        /// <method>Add(KeyValuePair&lt;string, string&gt; item)</method>
        public void Add(KeyValuePair<string, string> item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Add form item
        /// </summary>
        /// <param name="key">string</param>
        /// <param name="value">string</param>
        /// <method>Add(string key, string value)</method>
        public void Add(string key, string value)
        {
            _items.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>
        /// Submit form to url
        /// </summary>
        /// <param name="url">string</param>
        /// <method>Submit(string url)</method>
        public void Submit(string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", url);

            foreach (KeyValuePair<string, string> item in _items)
                sb.AppendFormat("<input type='hidden' name='{0}' value='{1}'>", item.Key, item.Value);

            sb.Append("</form></body></html>");

            byte[] buffer = Encoding.ASCII.GetBytes(sb.ToString());
            _response.Clear();
            _response.ContentType = "text/HTML";
            _response.BodyWriter.WriteAsync(buffer);
            _response.CompleteAsync();
        }
    }
}
