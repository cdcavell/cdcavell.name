using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Result Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/23/2020 | Initial build |~ 
    /// </revision>
    public class ResultModel
    {
        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NoContent;
        /// <value>string</value>
        public string StatusMessage { get; set; } = HttpStatusCode.NoContent.ToString();
        /// <value>int</value>
        public int MaxResults { get; set; } = 300;
        /// <value>int</value>
        public int DisplayCount { get; set; } = 15;
        /// <value>int</value>
        public int PageNumber { get; set; } = 1;
        /// <value>int</value>
        public int TotalPages { get { return (this.Items.Count() < this.DisplayCount) ? 1 : (this.Items.Count() / this.DisplayCount); } }
        /// <value>List&lt;ItemModel&gt;</value>
        public List<ItemModel> Items { get; set; } = new List<ItemModel>();

        private string _type;
        /// <value>string - excepted values (Web, Image or Video)</value>
        public string Type
        {
            get => _type;
            set
            {
                switch(value)
                {
                    case "Web":
                    case "Image":
                    case "Video":
                        _type = value;
                        break;
                    default:
                        throw new Exception("Invalid property type: " + value);
                }
            
            }
        }
    }
}
