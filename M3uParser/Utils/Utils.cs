using System;

namespace M3UParser.Utils
{
	public static class Helpers
	{
		public static string GetTitle(string line)
		{
			if (string.IsNullOrWhiteSpace(line)) throw new ArgumentException("Text input could not be empty or null.");
			var titleIndex = line.IndexOf(',');
			if (titleIndex == -1) throw new Exception($"Failed to find title in string: {line}");

			return line.Substring(titleIndex + 1);
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

				return propertyStartIndex.Substring(0, propertyLastIndex);
			}
			catch
			{
				return null;
			}
		}
	}
}