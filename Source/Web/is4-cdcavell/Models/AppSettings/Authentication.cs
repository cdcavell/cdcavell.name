namespace is4_cdcavell.Models.AppSettings
{
    /// <summary>
    /// Authentication model
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 10/09/2020 | Initial build |~ 
    /// </revision>
    public class Authentication
    {
        /// <value>Twitter</value>
        public Twitter Twitter { get; set; }
        /// <value>Facebook</value>
        public Facebook Facebook { get; set; }
        /// <value>Microsoft</value>
        public Microsoft Microsoft { get; set; }
        /// <value>Google</value>
        public Google Google { get; set; }
        /// <value>GitHub</value>
        public GitHub GitHub { get; set; }
        /// <value>LinkedIn</value>
        public LinkedIn LinkedIn { get; set; }
    }
}
