namespace M3UParser.Models
{
    public class M3uPlaylistEntry
    {
        /// <summary>
        /// Playlist entry title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Playlist entry path.
        /// </summary>
        public string Uri { get; set; }
        
        /// <summary>
        /// Playlist entry id.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Playlist entry logo uri.
        /// </summary>
        public string Logo { get; set; }
        
        /// <summary>
        /// Playlist entry name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Playlist entry small logo uri.
        /// </summary>
        public string SmallLogo { get; set; }
        
        /// <summary>
        /// Playlist entry group.
        /// </summary>
        public string Group { get; set; }
        
        /// <summary>
        /// Playlist entry group parent code.
        /// </summary>
        public string Code { get; set; }
    }
}