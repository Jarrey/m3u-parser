namespace M3UParser.Constants
{
    public static class Directives
    {
        /// <summary>
        /// File header, must be the first line of the file.
        /// </summary>
        public const string EXTM3U = "#EXTM3U";

        /// <summary>
        /// Track information: runtime in seconds and display title of the following resource.
        /// Additional properties as key-value pairs.
        /// </summary>
        public const string EXTINF = "#EXTINF";
    }
}