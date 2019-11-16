using M3UParser.Attributes;

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
        [PlaylistEntry("tvg-id")]
        public string Id { get; set; }
        
        /// <summary>
        /// Playlist entry logo uri.
        /// </summary>
        [PlaylistEntry("tvg-logo")]
        public string Logo { get; set; }
        
        /// <summary>
        /// Playlist entry name.
        /// </summary>
        [PlaylistEntry("tvg-name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Playlist entry small logo uri.
        /// </summary>
        [PlaylistEntry("tvg-logo-small")]
        public string SmallLogo { get; set; }
        
        /// <summary>
        /// Playlist entry group.
        /// </summary>
        [PlaylistEntry("group-title")]
        public string Group { get; set; }
        
        /// <summary>
        /// Playlist entry group parent code.
        /// </summary>
        [PlaylistEntry("parent-code")]
        public string Code { get; set; }
    }
}