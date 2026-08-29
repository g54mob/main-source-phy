using System;
using System.Collections.Generic;
using System.IO;

namespace Battlehub.Utils
{
	public static class PathHelper
	{
		public static bool IsPathRooted(string path)
		{
			return Path.IsPathRooted(path);
		}

		public static string GetRelativePath(string filespec, string folder)
		{
			Uri uri = new Uri(filespec);
			string text = folder;
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			if (!text.EndsWith(directorySeparatorChar.ToString()))
			{
				string text2 = folder;
				directorySeparatorChar = Path.DirectorySeparatorChar;
				folder = text2 + directorySeparatorChar;
			}
			return Uri.UnescapeDataString(new Uri(folder).MakeRelativeUri(uri).ToString().Replace('/', Path.DirectorySeparatorChar));
		}

		public static string RemoveInvalidFileNameCharacters(string name)
		{
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			for (int i = 0; i < invalidFileNameChars.Length; i++)
			{
				name = name.Replace(invalidFileNameChars[i].ToString(), string.Empty);
			}
			return name;
		}

		public static string GetUniqueName(string desiredName, string ext, List<string> existingNames, bool noSpace = false)
		{
			if (existingNames == null || existingNames.Count == 0)
			{
				return desiredName;
			}
			for (int i = 0; i < existingNames.Count; i++)
			{
				existingNames[i] = existingNames[i].ToLower();
			}
			HashSet<string> hashSet = new HashSet<string>(existingNames);
			if (string.IsNullOrEmpty(ext))
			{
				if (!hashSet.Contains(desiredName.ToLower()))
				{
					return desiredName;
				}
			}
			else if (!hashSet.Contains($"{desiredName.ToLower()}{ext}"))
			{
				return desiredName;
			}
			string text = desiredName.Split(' ')[^1];
			if (!int.TryParse(text, out var result))
			{
				result = 1;
			}
			else
			{
				desiredName = desiredName.Substring(0, desiredName.Length - text.Length).TrimEnd(' ');
			}
			for (int j = 0; j < 10000; j++)
			{
				string text2 = ((!string.IsNullOrEmpty(ext)) ? $"{desiredName} {result}{ext}" : $"{desiredName} {result}");
				if (noSpace)
				{
					text2 = text2.Replace(" ", "");
				}
				if (!hashSet.Contains(text2.ToLower()))
				{
					if (noSpace)
					{
						return $"{desiredName} {result}".Replace(" ", "");
					}
					return $"{desiredName} {result}";
				}
				result++;
			}
			return string.Format("{0} {1}", desiredName, Guid.NewGuid().ToString("N"));
		}

		public static string GetUniqueName(string desiredName, List<string> existingNames)
		{
			return GetUniqueName(desiredName, null, existingNames);
		}
	}
}
