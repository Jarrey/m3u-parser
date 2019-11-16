using System;

namespace M3UParser.Attributes
{
    public class PlaylistEntryAttribute : Attribute
    {
        public PlaylistEntryAttribute(string tag)  
        {  
            Tag = tag;
        }

        public readonly string Tag;
    }
}