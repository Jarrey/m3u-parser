using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using M3UParser.Constants;
using M3UParser.Models;
using M3UParser.Utils;

namespace M3UParser
{
    public static class M3uContent
    {
        public static string ToText(M3uPlaylist playlist)
        {
            var sb = new StringBuilder();

            sb.AppendLine(Directives.EXTM3U);

            foreach (var entry in playlist.PlaylistEntries)
            {
                sb.AppendLine(entry.Uri);
            }

//            sb = sb.Replace(Environment.NewLine, "", sb.Length - 3, 3);
            return sb.ToString();
        }

        public static M3uPlaylist GetFromStream(Stream stream)
        {
            var playlistEntries = new List<M3uPlaylistEntry>();

            var streamReader = new StreamReader(stream);

            var prevLineIsEXTINF = false;
            var title = string.Empty;
            var id = string.Empty;
            var name = string.Empty;
            var logo = string.Empty;
            var smallLogo = string.Empty;
            var groupTitle = string.Empty;
            var parentCode = string.Empty;

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                line.TrimStart();
                
                if (line.StartsWith(Directives.EXTINF))
                {
                    if (prevLineIsEXTINF)
                        throw new ArgumentNullException(
                            "Every EXTINF tag in a Playlist MUST have an media URI applied to it.");

                    prevLineIsEXTINF = true;
                    title = Helpers.GetTitle(line);
                    id = Helpers.GetProperties(line, Properties.TvgId);
                    name = Helpers.GetProperties(line, Properties.TvgName);
                    logo = Helpers.GetProperties(line, Properties.TvgLogo);
                    smallLogo = Helpers.GetProperties(line, Properties.TvgLogoSmall);
                    groupTitle = Helpers.GetProperties(line, Properties.GroupTitle);
                    parentCode = Helpers.GetProperties(line, Properties.ParentCode);

                    continue;
                }

                if (line.StartsWith("#")) continue;
                if (string.IsNullOrWhiteSpace(line)) continue;

                var playListEntry = new M3uPlaylistEntry
                {
                    Id = id,
                    Title = title,
                    Logo = logo,
                    SmallLogo = smallLogo,
                    Name = name,
                    Uri = line,
                    Group = groupTitle,
                    Code = parentCode
                };

                playlistEntries.Add(playListEntry);

                prevLineIsEXTINF = false;
                title = string.Empty;
                id = string.Empty;
                name = string.Empty;
                logo = string.Empty;
                smallLogo = string.Empty;
                groupTitle = string.Empty;
                parentCode = string.Empty;
            }
            
            var playlist = new M3uPlaylist
            {
                PlaylistEntries = playlistEntries
            };

            return playlist;
        }
    }
}