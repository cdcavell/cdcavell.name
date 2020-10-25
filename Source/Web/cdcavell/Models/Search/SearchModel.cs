﻿using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;

namespace cdcavell.Models.Search
{
    /// <summary>
    /// Search Model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/24/2020 | Initial build |~ 
    /// </revision>
    public class SearchModel
    {
        /// <value>HttpStatusCode</value>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NoContent;
        /// <value>int</value>
        public int Tab { get; set; } = 1;
        /// <value>string</value>
        public string WebActive { get; set; } = "active";
        /// <value>ResultModel</value>
        public ResultModel WebResult { get; set; } = new ResultModel();
        /// <value>string</value>
        public string ImageActive { get; set; } = string.Empty;
        /// <value>string</value>
        public string ImageDisabled { get { return (this.ImageResult.Items.Count > 0) ? string.Empty : "disabled"; } }
        /// <value>ResultModel</value>
        public ResultModel ImageResult { get; set; } = new ResultModel();
        /// <value>string</value>
        public string VideoActive { get; set; } = string.Empty;
        /// <value>string</value>
        public string VideoDisabled { get { return (this.VideoResult.Items.Count > 0) ? string.Empty : "disabled"; } }
        /// <value>ResultModel</value>
        public ResultModel VideoResult { get; set; } = new ResultModel();

        private string _searchRequest;
        /// <value>string</value>
        public string SearchRequest 
        {
            get => _searchRequest; 
            set { _searchRequest = HttpUtility.UrlEncode((value ?? string.Empty).Trim().Clean()); }
                
        }
    }
}
