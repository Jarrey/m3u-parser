using System;

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
    }
}