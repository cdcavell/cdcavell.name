namespace cdcavell.Models.Utilities
{
    /// <summary>
    /// YouTube video display model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.1 | 10/28/2020 | Initial build |~ 
    /// </revision>    
    public class YouTubeVideo
    {
        /// <value>string</value>
        public string Id { get; set; } 
        /// <value>string</value>
        public string VideoUrl { get { return "https://www.youtube.com/embed/" + this.Id.Trim() + "?autoplay=1&controls=0"; } }
        /// <value>string</value>
        public string ImageUrl { get { return "https://img.youtube.com/vi/" + this.Id.Trim() + "/hqdefault.jpg"; } }
        /// <value>string</value>
        public string RedirectUrl { get { return "https://www.youtube.com/watch?v=" + this.Id.Trim(); } }
    }
}
