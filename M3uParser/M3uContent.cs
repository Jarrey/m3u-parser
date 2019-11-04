using System;
using System.IO;
using System.Text;
using M3UParser.Constants;
using M3UParser.Models;
using M3UParser.Utils;

namespace M3UParser
{
    public class M3uContent
    {
        public string ToText(M3uPlaylist playlist)
        {
            var sb = new StringBuilder();

            sb.AppendLine(Directives.EXTM3U);

            foreach (var entry in playlist.PlaylistEntries)
            {
                sb.AppendLine(entry.Uri);
            }

            sb = sb.Replace(Environment.NewLine, "", sb.Length - 3, 3);
            return sb.ToString();
        }

        public M3uPlaylist GetFromStream(Stream stream)
        {
            var playlist = new M3uPlaylist();
            var streamReader = new StreamReader(stream);

            var prevLineIsEXTINF = false;
            var title = string.Empty;

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                // TODO: Line could start with white space followed by # or uri or text. Need to trim that space
                if (line.StartsWith(Directives.EXTINF))
                {
                    prevLineIsEXTINF = true;
                    title = Helpers.GetTitle(line);
                    continue;
                }

                if (line.StartsWith("#")) continue;
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (prevLineIsEXTINF && string.IsNullOrWhiteSpace(line))
                    throw new ArgumentNullException(
                        "Every EXTINF tag in a Playlist MUST have an media URI applied to it.");

                var playListEntry = new M3uPlaylistEntry
                {
                    Uri = line,
                    Title = title
                };

                playlist.PlaylistEntries.Add(playListEntry);

                prevLineIsEXTINF = false;
                title = string.Empty;
            }

            return playlist;
        }
    }
}