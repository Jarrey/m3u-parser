using System;
using System.IO;
using System.Reflection;
using M3UParser.Constants;
using M3UParser.Models;
using M3UParser.Utils;
using Xunit;

namespace M3uParser.Tests
{
    public class UtilsTests
    {
        private static string ReadFile() => File.ReadAllText(
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MockData", "playlist.m3u"));
        
        [Fact]
        public void ShouldChainFullAttributesList()
        {
            var m3uPlaylistEntry = new M3uPlaylistEntry
            {
                Title = "TVOne",
                Uri = "http://local",
                Name = "TvOne",
                Logo = "http://logo",
                SmallLogo = "http://local-small-logo",
                Code = "123",
                Group = "local",
                Id = "12"
            };
            var title = Helpers.ChainAttributes(m3uPlaylistEntry);
            Assert.Equal(
                "#EXTINF:-1 tvg-id=\"12\" tvg-logo=\"http://logo\" tvg-name=\"TvOne\" tvg-logo-small=\"http://local-small-logo\" group-title=\"local\" parent-code=\"123\", TVOne", 
                title);
        }
        
        [Fact]
        public void ShouldChainAttributes()
        {
            var m3uPlaylistEntry = new M3uPlaylistEntry
            {
                Title = "TVOne",
                Logo = "http://logo"
            };
            var title = Helpers.ChainAttributes(m3uPlaylistEntry);
            Assert.Equal(
                "#EXTINF:-1 tvg-logo=\"http://logo\", TVOne", 
                title);
        }

        [Fact]
        public void ShouldGetPlaylistTitle()
        {
            var text = ReadFile();
            var title = Helpers.GetTitle(text);
            var newLineIndex = title.IndexOf("\n");

            Assert.Equal("TV Channel1", title.Substring(0, newLineIndex));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ShouldGetPlaylistTitleThrowException_if_TextIsNullOrWhitespace(string text)
        {
            var exception = Assert.Throws<ArgumentException>(() => Helpers.GetTitle(text));
            Assert.Equal("Text input could not be empty or null.", exception.Message);
        }

        [Fact]
        public void ShouldGetPlaylistTitleThrowException_if_TitleNotFound()
        {
            const string text = "#EXTINF:-1";
            var exception = Assert.Throws<Exception>(() => Helpers.GetTitle(text));
            Assert.Equal($"Failed to find title in string: {text}", exception.Message);
        }

        [Theory]
        [InlineData(Properties.GroupTitle, "titleOne")]
        [InlineData(Properties.ParentCode, "1234")]
        [InlineData(Properties.TvgId, "1")]
        [InlineData(Properties.TvgLogo, "https://localhost1")]
        [InlineData(Properties.TvgLogoSmall, "https://localhost1small")]
        [InlineData(Properties.TvgName, "TV Channel1")]
        public void ShouldGetPlaylistProperties(string propertyName, string result)
        {
            var text = ReadFile();
            var propertyValue = Helpers.GetProperties(text, propertyName);
            Assert.Equal(result, propertyValue);
        }

        [Theory]
        [InlineData("", Properties.GroupTitle)]
        [InlineData(" ", Properties.ParentCode)]
        [InlineData(null, Properties.TvgId)]
        public void ShouldGetPlaylistPropertiesThrowException_if_TextIsNullOrWhitespace(string text,
            string propertyName)
        {
            var exception = Assert.Throws<ArgumentException>(() => Helpers.GetProperties(text, propertyName));
            Assert.Equal("Text input could not be empty or null.", exception.Message);
        }

        [Fact]
        public void ShouldGetPlaylistPropertiesReturnNull()
        {
            const string text = "#EXTINF:-1 parent-code=\"1234\",TV Channel1";
            var propertyValue = Helpers.GetProperties(text, Properties.GroupTitle);
            Assert.Null(propertyValue);
        }
    }
}