using System.Collections.Generic;

namespace M3UParser.Models
{
	public class M3uPlaylist
	{
		/// <summary>
		/// List of playlist entries.
		/// </summary>
		public List<M3uPlaylistEntry> PlaylistEntries { get; }
	}
}