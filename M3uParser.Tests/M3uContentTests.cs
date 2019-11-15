using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using M3UParser;
using M3UParser.Models;
using Xunit;

namespace M3uParser.Tests
{
    public class M3uContentTests
    {
        private static string ReadFile(string filename) => File.ReadAllText(
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MockData", filename));

        private static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
        
        [Fact]
        public void ShouldConvertPlaylistToClass()
        {
            var text = ReadFile("playlist.m3u");
            var stream = GenerateStreamFromString(text);
            var content = M3uContent.GetFromStream(stream);
            
            Assert.IsType<List<M3uPlaylistEntry>>(content.PlaylistEntries);
            Assert.Equal(3, content.PlaylistEntries.Count);
           
            Assert.Equal("titleOne", content.PlaylistEntries[0].Group);
            Assert.Equal("1", content.PlaylistEntries[0].Id);
            Assert.Equal("TV Channel1", content.PlaylistEntries[0].Name);
            Assert.Equal("https://localhost1", content.PlaylistEntries[0].Logo);
            Assert.Equal("https://localhost1small", content.PlaylistEntries[0].SmallLogo);
            Assert.Equal("1234", content.PlaylistEntries[0].Code);
            Assert.Equal("TV Channel1", content.PlaylistEntries[0].Title);
            Assert.Equal("udp://@111.222.33.4:1234", content.PlaylistEntries[0].Uri);
        }
        
        [Fact]
        public void ShouldThrowErrorIfPlaylistIsNotValid()
        {
            var text = ReadFile("invalidPlaylist.m3u");
            var stream = GenerateStreamFromString(text);

            var exception = Assert.Throws<ArgumentNullException>(() => M3uContent.GetFromStream(stream));
            Assert.Contains("Every EXTINF tag in a Playlist MUST have an media URI applied to it.", exception.Message);
        }
    }
}