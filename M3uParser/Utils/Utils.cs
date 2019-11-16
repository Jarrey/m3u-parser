using System;
using System.Collections.Generic;
using System.Reflection;
using M3UParser.Attributes;
using M3UParser.Constants;
using M3UParser.Models;

namespace M3UParser.Utils
{
    public static class Helpers
    {
        public static string ChainAttributes(M3uPlaylistEntry entry)
        {
            var properties = entry.GetType().GetProperties();
            var joinedProperty = new List<string>();
            var title = string.Empty;

            foreach (var property in properties)
            {
                if (property.Name == "Uri") continue;
                var value = property.GetValue(entry, null);
                if (property.Name == "Title")
                {
                    title = $"{value}";
                    continue;
                }
                if (string.IsNullOrWhiteSpace(value?.ToString())) continue;
                var attribute = property.GetCustomAttribute<PlaylistEntryAttribute>();

                joinedProperty.Add(attribute != null ? $"{attribute.Tag}=\"{value}\"" : $"{value}");
            }

            var serializedProperties = string.Join(" ", joinedProperty);
            
            var serializedData = $"{Directives.EXTINF}:-1 {serializedProperties}, {title}";

            return serializedData;
        }
        
        public static string GetTitle(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) throw new ArgumentException("Text input could not be empty or null.");
            var titleIndex = line.IndexOf(',');
            if (titleIndex == -1) throw new Exception($"Failed to find title in string: {line}");

            return line.Substring(titleIndex + 1).Trim();
        }

        public static string GetProperties(string line, string property)
        {
            if (string.IsNullOrWhiteSpace(line)) throw new ArgumentException("Text input could not be empty or null.");

            try
            {
                if (!line.Contains(property))
                    throw new ArgumentException(
                        $"Property {property} not found in line: {line}.");

                var propertyStartIndex = line.Substring(line.IndexOf(property) + property.Length + 2);
                var propertyLastIndex = propertyStartIndex.IndexOf("\"");

                return propertyStartIndex.Substring(0, propertyLastIndex).Trim();
            }
            catch
            {
                return null;
            }
        }
    }
}